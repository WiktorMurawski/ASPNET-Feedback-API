using Microsoft.AspNetCore.Mvc;
using Pre_Trainee_Task.DTOs;
using Pre_Trainee_Task.Services;

namespace Pre_Trainee_Task.Controllers;

/// <summary>
///     Handles authentication-related operations - user registration and login
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;

    /// <summary>
    ///     Initializes a new instance of the controller
    /// </summary>
    /// <param name="authService">Service for handling authentication logic</param>
    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    /// <summary>
    ///     Registers a new user
    /// </summary>
    /// <param name="dto">The user registration data</param>
    /// <returns> The registered user's email if registration was successful</returns>
    /// <response code="200">User registered successfully</response>
    /// <response code="400">Invalid request data or registration error</response>
    [HttpPost("register")]
    public async Task<IActionResult> Register(UserDto? dto)
    {
        if (dto == null) return BadRequest();

        try
        {
            var user = await _authService.Register(dto);
            return Ok(new { user.Email });
        }
        catch (ArgumentException e)
        {
            return BadRequest(e.Message);
        }
        catch (InvalidOperationException e)
        {
            return BadRequest(e.Message);
        }
    }

    /// <summary>
    ///     Authenticates a user and returns a JWT token
    /// </summary>
    /// <param name="dto">The user login data</param>
    /// <returns> JWT token if authentication is successful </returns>
    /// <response code="200">User authenticated successfully</response>
    /// <response code="401">Authentication failed</response>
    [HttpPost("login")]
    public async Task<IActionResult> Login(UserDto? dto)
    {
        if (dto == null) return BadRequest();

        try
        {
            var token = await _authService.Login(dto);
            return Ok(new { token });
        }
        catch (UnauthorizedAccessException e)
        {
            return Unauthorized(e.Message);
        }
        catch (ArgumentException e)
        {
            return BadRequest(e.Message);
        }
    }
}
