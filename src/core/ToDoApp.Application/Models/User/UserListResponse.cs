using System;
using System.Collections.Generic;
using System.Text;
using ToDoApp.Persistence.Data;

namespace ToDoApp.Application.Models.User
{
public    class UserListResponse:ResponseBase
    {
        private List<TodoUserInfo> _list { get; set; }
        public List<TodoUserInfo> List
        {
            get
            {
                if (_list == null)
                    _list = new List<TodoUserInfo>();
                return _list;
            }
            set
            {
                _list = value;
            }
        }
    }
}
