using Pre_Trainee_Task.DTOs;
using Pre_Trainee_Task.Models;

namespace Pre_Trainee_Task.Services;

public interface IAuthService
{
    Task<User> Register(UserDto dto);
    Task<string> Login(UserDto dto);
}
