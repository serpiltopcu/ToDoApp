namespace ToDoApp.Application.Models.Todo
{
    public class DeleteTodoRequest
    {
        public string TodoId { get; set; }
        private TodoUserInfoModel _userInfo { get; set; }
        public TodoUserInfoModel UserInfo
        {
            get
            {
                if (_userInfo == null)
                    _userInfo = new TodoUserInfoModel();
                return _userInfo;
            }
            set
            {
                _userInfo = value;
            }

        }
    }
}
