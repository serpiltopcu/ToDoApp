using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Couchbase.Linq;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using ToDoApp.Application.Interfaces;
using ToDoApp.Web.Models.User;

namespace ToDoApp.Web.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }


        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register([FromBody] RegisterRequestModel arg)
        {
            var model = _userService.Register(new Application.Models.User.RegisterRequest()
            {
                Name = arg.Name,
                Email = arg.Email,
                Password = arg.Password
            });
            
            RegisterResponseModel response = new RegisterResponseModel()
            {
                Status = model.Status,
                Message = model.Message
            };
            if (response.Status)
            {
                var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, arg.Name),
                new Claim(ClaimTypes.NameIdentifier, model.Id),
            };

                var userIdentity = new ClaimsIdentity(claims, "login");

                ClaimsPrincipal principal = new ClaimsPrincipal(userIdentity);
                await HttpContext.SignInAsync(principal);
            }
            
            return Json(response);
        }

        [HttpPost]
        public async Task<IActionResult> Login([FromBody]LoginRequestModel arg)
        {
            var model = _userService.Login(new Application.Models.User.LoginRequest()
            {
                Email = arg.Email,
                Password = arg.Password
            });
            LoginResponseModel response = new LoginResponseModel()
            {
                Status = model.Status,
                Message = model.Message
            };

            if (response.Status)
            {
                var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, model.Name),
                new Claim(ClaimTypes.NameIdentifier, model.Id)
            };

                var userIdentity = new ClaimsIdentity(claims, "login");

                ClaimsPrincipal principal = new ClaimsPrincipal(userIdentity);
                await HttpContext.SignInAsync(principal);
            }

            
            return Json(response);
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();

            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult GetList()
        {
            return Json(_userService.GetList());
        }

    }
}