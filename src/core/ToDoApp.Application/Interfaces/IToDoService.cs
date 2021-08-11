using ToDoApp.Application.Models.Todo;

namespace ToDoApp.Application.Interfaces
{
    public interface IToDoService
    {
        AddTodoResponse Add(AddTodoRequest arg);
        ToDoListResponse GetList();
        DeleteTodoResponse Delete(DeleteTodoRequest arg);
    }
}
