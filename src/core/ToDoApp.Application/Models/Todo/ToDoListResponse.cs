using System.Collections.Generic;
using ToDoApp.Persistence.Data;

namespace ToDoApp.Application.Models.Todo
{
    public class ToDoListResponse : ResponseBase
    {
        private List<TodoList> _list { get; set; }
        public List<TodoList> List
        {
            get
            {
                if (_list == null)
                    _list = new List<TodoList>();
                return _list;
            }
            set
            {
                _list = value;
            }
        }
    }
}
