using System;
using System.Collections.Generic;

namespace ToDoApp.Web.Models.ToDo
{
    public class TodoListResponseModel: ResponseBaseModel
    {
        private List<TodoListInfoResponseModel> _list { get; set; }
        public List<TodoListInfoResponseModel> List
        {
            get
            {
                if (_list == null)
                    _list = new List<TodoListInfoResponseModel>();
                return _list;
            }
            set
            {
                _list = value;
            }
        }
    }

    public class TodoListInfoResponseModel
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? DueDate { get; set; }
        public Application.Models.Todo.TodoStatusEnum Status { get; set; }

        private TodoUserInfoModel _createdByUser { get; set; }

        public TodoUserInfoModel CreatedByUser
        {
            get
            {
                if (_createdByUser == null)
                    _createdByUser = new TodoUserInfoModel();
                return _createdByUser;
            }
            set
            {
                _createdByUser = value;
            }

        }
        private TodoUserInfoModel _assignedToUser { get; set; }
        public TodoUserInfoModel AssignedToUser
        {
            get
            {
                if (_assignedToUser == null)
                    _assignedToUser = new TodoUserInfoModel();
                return _assignedToUser;
            }
            set
            {
                _assignedToUser = value;
            }
        }
    }

}
