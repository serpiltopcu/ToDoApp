using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ToDoApp.Web.Models.User
{
    public class LoginRequestModel
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
