using System;

namespace ToDoApp.Domain.Entities
{
    public class ToDoList
    {
        public string Title{ get; set; }
        public string Content { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime DueDate { get; set; }
    }
}
