using Pre_Trainee_Task.DTOs;
using Pre_Trainee_Task.Models;

namespace Pre_Trainee_Task.Services;

public interface IAuthService
{
    User Register(UserDto dto);
    string Login(UserDto dto);
}
