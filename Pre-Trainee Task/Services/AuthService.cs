using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Pre_Trainee_Task.Config;
using Pre_Trainee_Task.Data;
using Pre_Trainee_Task.DTOs;
using Pre_Trainee_Task.Models;

namespace Pre_Trainee_Task.Services;

public class AuthService : IAuthService
{
    private readonly FeedbackDbContext _context;
    private readonly JwtConfig _jwtConfig;

    public AuthService(FeedbackDbContext context, IOptions<JwtConfig> jwtConfig)
    {
        _context = context;
        _jwtConfig = jwtConfig.Value;
    }

    private void ValidateInput(UserDto dto)
    {
        if (dto == null)
            throw new ArgumentNullException(nameof(dto), "UserDto cannot be null");

        if (string.IsNullOrWhiteSpace(dto.Email))
            throw new ArgumentException("Email cannot be null or empty", nameof(dto.Email));

        if (string.IsNullOrWhiteSpace(dto.Password))
            throw new ArgumentException("Password cannot be null or empty", nameof(dto.Password));
        
        if(dto.Password.Length < 8 || dto.Password.Length > 50)
            throw new ArgumentOutOfRangeException(nameof(dto.Password), "Password must be between 8 and 50 characters");
    }
    
    public async Task<User> Register(UserDto dto)
    {
        ValidateInput(dto);
        
        var existingUser = await _context.Users.FirstOrDefaultAsync(u =>
            u.Email == dto.Email);
        if (existingUser != null) throw new InvalidOperationException("User already exists");

        var hashedPassword = BCrypt.Net.BCrypt.HashPassword(dto.Password);
        var newUser = new User
        {
            Email = dto.Email,
            PasswordHash = hashedPassword,
            Role = UserRole.User
        };

        await _context.Users.AddAsync(newUser);
        await _context.SaveChangesAsync();

        return newUser;
    }

    public async Task<string> Login(UserDto dto)
    {
        ValidateInput(dto);
        
        var user = await _context.Users.FirstOrDefaultAsync(u =>
            u.Email == dto.Email);
        if (user == null ||
            !BCrypt.Net.BCrypt.Verify(dto.Password, user.PasswordHash))
            throw new UnauthorizedAccessException("Invalid credentials");

        Claim[] claims =
        [
            new(ClaimTypes.Email, user.Email),
            new(ClaimTypes.Role, user.Role.ToString()),
            new(ClaimTypes.Actor, user.Id.ToString())
        ];

        var key =
            new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtConfig.Key));
        var creds =
            new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            _jwtConfig.Issuer,
            _jwtConfig.Audience,
            claims,
            expires: DateTime.UtcNow.AddDays(1),
            signingCredentials: creds
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}
