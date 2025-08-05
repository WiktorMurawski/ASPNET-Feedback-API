using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using Pre_Trainee_Task.Data;
using Pre_Trainee_Task.DTOs;
using Pre_Trainee_Task.Models;

namespace Pre_Trainee_Task.Services;

public class AuthService : IAuthService
{
    private readonly IConfiguration _config;
    private readonly FeedbackDbContext _context;

    public AuthService(FeedbackDbContext context, IConfiguration config)
    {
        _context = context;
        _config = config;
    }

    public User Register(UserDto dto)
    {
        var existingUser = _context.Users.FirstOrDefault(u =>
            u.Email == dto.Email);
        if (existingUser != null) throw new Exception("User already exists");

        var hashedPassword = BCrypt.Net.BCrypt.HashPassword(dto.Password);
        var newUser = new User
        {
            Email = dto.Email,
            PasswordHash = hashedPassword,
            Role = UserRole.User
        };

        _context.Users.Add(newUser);
        _context.SaveChanges();

        return newUser;
    }

    public string Login(UserDto dto)
    {
        var user = _context.Users.FirstOrDefault(u =>
            u.Email == dto.Email);
        if (user == null ||
            !BCrypt.Net.BCrypt.Verify(dto.Password, user.PasswordHash))
            throw new Exception("Invalid credentials");

        // Prepare claims
        var claims = new[]
        {
            new Claim(ClaimTypes.Email, user.Email),
            new Claim(ClaimTypes.Role, user.Role.ToString())
        };

        // Get key from config
        var key =
            new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        // Create the token
        var token = new JwtSecurityToken(
            _config["Jwt:Issuer"],
            _config["Jwt:Audience"],
            claims,
            expires: DateTime.UtcNow.AddHours(1),
            signingCredentials: creds
        );

        // Return the token as a string
        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}
