using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ToDoApp.Application.Interfaces;
using ToDoApp.Web.Models.ToDo;

namespace ToDoApp.Web.Controllers
{
    public class TodoController : Controller
    {
        private readonly IToDoService _toDoService;
        public TodoController(IToDoService toDoService)
        {
            _toDoService = toDoService;
        }

        public IActionResult Index()
        {
            return View();
        }

        [Authorize]
        [HttpPost]
        public IActionResult Add([FromBody]AddTodoRequestModel arg)
        {
            AddTodoResponseModel response = new AddTodoResponseModel();

            if (arg != null && arg.AssignedToUser.Id!=null)
            {
                var model = _toDoService.Add(new Application.Models.Todo.AddTodoRequest()
                {
                    AssignedToUser = new Application.Models.Todo.TodoUserInfoModel()
                    {
                        Id = arg.AssignedToUser.Id,
                        Name = arg.AssignedToUser.Name
                    },
                    CreatedByUser = new Application.Models.Todo.TodoUserInfoModel()
                    {
                        Id = User.FindFirstValue(ClaimTypes.NameIdentifier),
                        Name = User.Identity.Name
                    },
                    Content = arg.Content,
                    DueDate = arg.DueDate,
                    Title = arg.Title
                });

                response.Status = true;
            }
            else
            {
                response.Status = false;
                response.Message = "Model is empty!";
            }

            return Json(response);
        }


        [HttpGet]
        public IActionResult GetList()
        {
            TodoListResponseModel response = new TodoListResponseModel();

            var model = _toDoService.GetList();
            response.List = model.List.Select(a => new TodoListInfoResponseModel()
            {
                Id = a.Id,
                Title = a.Title,
                Content = a.Content,
                DueDate = a.DueDate,
                AssignedToUser = new TodoUserInfoModel()
                {
                    Id = a.AssignedToUser.Id,
                    Name = a.AssignedToUser.Name
                },
                CreatedByUser = new TodoUserInfoModel()
                {
                    Id = a.CreatedByUser.Id,
                    Name = a.CreatedByUser.Name
                }
            }).ToList();

            response.Status = model.Status;
            response.Message = model.Message;
            return Json(response);
        }

        [Authorize]
        [HttpPost]
        public IActionResult Delete([FromBody]string id)
        {
            DeleteTodoResponseModel response = new DeleteTodoResponseModel();
            if (string.IsNullOrEmpty(id))
            {
                response.Status = false;
                response.Message = "This to do could not be found!";

            }
            else
            {
                var model = _toDoService.Delete(new Application.Models.Todo.DeleteTodoRequest()
                {

                    TodoId = id,
                    UserInfo = new Application.Models.Todo.TodoUserInfoModel()
                    {
                        Id = User.FindFirstValue(ClaimTypes.NameIdentifier),
                        Name = User.Identity.Name
                    }
                });

                response.Status = model.Status;
                response.Message = model.Message;
            }

            return Json(response);
        }

    }
}