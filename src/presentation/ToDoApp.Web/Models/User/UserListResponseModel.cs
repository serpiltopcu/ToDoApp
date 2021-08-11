using System.Collections.Generic;
using ToDoApp.Web.Models.ToDo;

namespace ToDoApp.Web.Models.User
{
    public class UserListResponseModel
    {
        private List<TodoUserInfoModel> _list { get; set; }
        public List<TodoUserInfoModel> List
        {
            get
            {
                if (_list == null)
                    _list = new List<TodoUserInfoModel>();
                return _list;
            }
            set
            {
                _list = value;
            }
        }
    }
}
