using Microsoft.AspNetCore.Mvc;
using Pre_Trainee_Task.DTOs;
using Pre_Trainee_Task.Services;

namespace Pre_Trainee_Task.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    [HttpPost("register")]
    public IActionResult Register(UserDto? dto)
    {
        if (dto == null) return BadRequest();

        try
        {
            var user = _authService.Register(dto);
            return Ok(new { user.Email });
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpPost("login")]
    public IActionResult Login(UserDto? dto)
    {
        if (dto == null) return BadRequest();

        try
        {
            var token = _authService.Login(dto);
            return Ok(new { token });
        }
        catch (Exception e)
        {
            return Unauthorized(e.Message);
        }
    }
}
