namespace ToDoApp.Application.Models
{
    public abstract class ResponseBase
    {
        public bool Status { get; set; }
        public string Message { get; set; }
    }
}
