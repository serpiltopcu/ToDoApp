using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace ToDoApp.Persistence.Data
{
    public class TodoList
    {
        [Key]
        [JsonProperty("id")]
        public string Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        private TodoUserInfo _createdByUser { get; set; }
        public TodoUserInfo CreatedByUser
        {
            get
            {
                if (_createdByUser == null)
                    _createdByUser = new TodoUserInfo();
                return _createdByUser;
            }
            set
            {
                _createdByUser = value;
            }

        }
        private TodoUserInfo _updatedByUser { get; set; }
        public TodoUserInfo UpdatedByUser
        {
            get
            {
                if (_updatedByUser == null)
                    _updatedByUser = new TodoUserInfo();
                return _updatedByUser;
            }
            set
            {
                _updatedByUser = value;
            }

        }
        private TodoUserInfo _assignedToUser { get; set; }
        public TodoUserInfo AssignedToUser
        {
            get
            {
                if (_assignedToUser == null)
                    _assignedToUser = new TodoUserInfo();
                return _assignedToUser;
            }
            set
            {
                _assignedToUser = value;
            }
        }
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public DateTime? DueDate { get; set; }
        public string Status { get; set; }
    }

    public class TodoUserInfo
    {
        public string Id { get; set; }
        public string Name { get; set; }
    }
}
