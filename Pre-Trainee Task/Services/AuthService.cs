using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.Text;
using Pre_Trainee_Task.Data;
using Pre_Trainee_Task.DTOs;
using Pre_Trainee_Task.Models;

namespace Pre_Trainee_Task.Services;

public class AuthService : IAuthService
{
    private readonly FeedbackDbContext _context;
    private readonly IConfiguration _config;

    public AuthService(FeedbackDbContext context, IConfiguration config)
    {
        _context = context;
        _config = config;
    }
    
    public User Register(UserDto dto)
    {
        User? existingUser = _context.Users.FirstOrDefault(u => 
            u.Email == dto.Email);
        if (existingUser != null)
        {
            throw new Exception("User already exists");
        }

        string hashedPassword = BCrypt.Net.BCrypt.HashPassword(dto.Password);
        User newUser = new User
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
        User? user = _context.Users.FirstOrDefault(u => 
            u.Email == dto.Email);
        if (user == null || !BCrypt.Net.BCrypt.Verify(dto.Password, user.PasswordHash))
        {
            throw new Exception("Invalid credentials");
        }
        
        // Prepare claims
        var claims = new[]
        {
            new Claim(ClaimTypes.Email, user.Email),
            new Claim(ClaimTypes.Role, user.Role.ToString())
        };

        // Get key from config
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        // Create the token
        var token = new JwtSecurityToken(
            issuer: _config["Jwt:Issuer"],
            audience: _config["Jwt:Audience"],
            claims: claims,
            expires: DateTime.UtcNow.AddHours(1),
            signingCredentials: creds
        );

        // Return the token as a string
        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}
