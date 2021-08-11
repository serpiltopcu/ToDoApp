namespace ToDoApp.Web.Models
{
    public abstract class ResponseBaseModel
    {
        public bool Status { get; set; }
        public string Message { get; set; }
    }
}
