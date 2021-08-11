using System;

namespace ToDoApp.Application.Models.Todo
{
    public class AddTodoRequest
    {
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime? DueDate { get; set; }
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

    public class TodoUserInfoModel
    {
        public string Id { get; set; }
        public string Name { get; set; }

    }
}
