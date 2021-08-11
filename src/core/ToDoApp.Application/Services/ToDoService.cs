using Couchbase.Core;
using Couchbase.Extensions.DependencyInjection;
using Couchbase.Linq;
using System;
using System.Linq;
using ToDoApp.Application.Interfaces;
using ToDoApp.Application.Models;
using ToDoApp.Application.Models.Todo;
using ToDoApp.Persistence.Data;

namespace ToDoApp.Application.Services
{
    public class ToDoService : IToDoService
    {
        private readonly BucketContext _bucketContext;

        public ToDoService(BucketContext bucketProvider)
        {
            _bucketContext = bucketProvider;
        }


        public AddTodoResponse Add(AddTodoRequest arg)
        {
            AddTodoResponse response = new AddTodoResponse();

            try
            {
                TodoList todoModel = new TodoList()
                {
                    Id = Guid.NewGuid().ToString(),
                    Title = arg.Title,
                    Content = arg.Content,
                    DueDate = arg.DueDate,
                    CreatedDate = DateTime.Now.ToUniversalTime(),
                    Status = TodoStatusEnum.Active.ToString()
                };

                todoModel.AssignedToUser = new TodoUserInfo()
                {
                    Id = arg.AssignedToUser.Id,
                    Name = arg.AssignedToUser.Name
                };

                todoModel.CreatedByUser = new TodoUserInfo()
                {
                    Id = arg.CreatedByUser.Id,
                    Name = arg.CreatedByUser.Name
                };

              
                _bucketContext.Save(todoModel);
                response.Status = true;
            }
            catch (Exception ex)
            {
                response.Status = false;
                response.Message = ex.Message;
            }
            return response;
        }

        public ToDoListResponse GetList()
        {
            ToDoListResponse response = new ToDoListResponse();
            try
            {
                response.List = _bucketContext.Query<TodoList>().Where(a => a.Status == TodoStatusEnum.Active.ToString()).Select(a => new TodoList()
                {
                    Id = a.Id,
                    Title = a.Title,
                    Content = a.Content,
                    CreatedDate = a.CreatedDate,
                    AssignedToUser = a.AssignedToUser,
                    CreatedByUser = a.CreatedByUser,
                    DueDate = a.DueDate,
                    Status = a.Status
                }
                ).OrderBy(a=>a.CreatedDate).ToList();

                response.Status = true;
            }
            catch (Exception ex)
            {
                response.Status = false;
                response.Message = ex.Message;
            }

            return response;
        }

        public DeleteTodoResponse Delete(DeleteTodoRequest arg)
        {
            DeleteTodoResponse response = new DeleteTodoResponse();

            try
            {
                var todo = _bucketContext.Query<TodoList>().FirstOrDefault(a => a.Id == arg.TodoId);
                if (todo == null)
                {
                    response.Status = false;
                    response.Message = "This to do could not be found!";
                }
                else
                {
                    todo.Status = TodoStatusEnum.Passive.ToString();
                    todo.UpdatedByUser = new TodoUserInfo()
                    {
                        Id = arg.UserInfo.Id,
                        Name = arg.UserInfo.Name
                    };

                    _bucketContext.Save(todo);
                    response.Status = true;
                }

            }
            catch (Exception ex)
            {
                response.Status = false;
                response.Message = ex.Message;
            }

            return response;
        }
    }
}
