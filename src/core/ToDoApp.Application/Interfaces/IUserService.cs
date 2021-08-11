using ToDoApp.Application.Models.User;

namespace ToDoApp.Application.Interfaces
{
    public interface IUserService
    {
        RegisterResponse Register(RegisterRequest arg);
        LoginResponse Login(LoginRequest arg);
        UserListResponse GetList();
    }
}
