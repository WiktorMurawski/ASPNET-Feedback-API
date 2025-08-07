using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.Extensions.Options;
using Microsoft.EntityFrameworkCore;
using Pre_Trainee_Task.Config;
using Pre_Trainee_Task.Data;
using Pre_Trainee_Task.DTOs;
using Pre_Trainee_Task.Models;
using Pre_Trainee_Task.Services;

namespace TestProject.Tests
{
    public class AuthServiceTests
    {
        public AuthService GetService(out FeedbackDbContext context,
            out IOptions<JwtConfig> jwtConfig)
        {
            var options = new DbContextOptionsBuilder<FeedbackDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            context = new FeedbackDbContext(options);
            jwtConfig = Options.Create(new JwtConfig
            {
                Key = "supersecretjwtkeyforunittestingpurposes",
                Issuer = "TestIssuer",
                Audience = "TestAudience"
            });

            return new AuthService(context, jwtConfig);
        }

        [Fact]
        public async Task Register_ShouldCreateNewUser_WhenUserDoesNotExist()
        {
            var service = GetService(out var context, out var jwtConfig);

            var dto = new UserDto
            {
                Email = "test@example.com",
                Password = "Password123"
            };

            var result = await service.Register(dto);

            Assert.NotNull(result);
            Assert.Equal(dto.Email, result.Email);
            Assert.True(
                BCrypt.Net.BCrypt.Verify(dto.Password, result.PasswordHash));
        }

        [Fact]
        public async Task Register_ShouldThrowException_WhenUserAlreadyExists()
        {
            var service = GetService(out var context, out var jwtConfig);

            await context.Users.AddAsync(new User
            {
                Email = "existing@example.com",
                PasswordHash = BCrypt.Net.BCrypt.HashPassword("password")
            });
            await context.SaveChangesAsync();

            var dto = new UserDto
            {
                Email = "existing@example.com",
                Password = "Password123"
            };

            await Assert.ThrowsAsync<InvalidOperationException>(() => service.Register(dto));
        }

        [Fact]
        public async Task Login_ShouldReturnJWTToken_WhenCredentialsAreValid()
        {
            var service = GetService(out var context, out var jwtConfig);

            var email = "test@test.com";
            var password = "testpassword";
            var user = new User
            {
                Email = email,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(password),
                Role = UserRole.User
            };
            await context.Users.AddAsync(user);
            await context.SaveChangesAsync();

            var dto = new UserDto
            {
                Email = email,
                Password = password
            };

            var token = await service.Login(dto);
            var handler = new JwtSecurityTokenHandler();
            var jwt = handler.ReadJwtToken(token);

            Assert.False(string.IsNullOrWhiteSpace(token));
            Assert.Equal(email,
                jwt.Claims.First(c => c.Type == ClaimTypes.Email).Value);
            Assert.Equal(UserRole.User.ToString(),
                jwt.Claims.First(c => c.Type == ClaimTypes.Role).Value);
            Assert.Equal(jwtConfig.Value.Issuer, jwt.Issuer);
        }

        [Fact]
        public async Task Login_ShouldThrowException_WhenEmailIsNotRegistered()
        {
            var service = GetService(out var context, out var jwtConfig);

            var dto = new UserDto
            {
                Email = "notregistered@email.com",
                Password = "something"
            };

            await Assert.ThrowsAsync<UnauthorizedAccessException>(() => service.Login(dto));
        }

        [Fact]
        public async Task Login_ShouldThrowException_WhenPasswordIsIncorrect()
        {
            var service = GetService(out var context, out var jwtConfig);

            await context.Users.AddAsync(new User
            {
                Email = "user@test.com",
                PasswordHash =
                    BCrypt.Net.BCrypt.HashPassword("CorrectPassword"),
                Role = UserRole.User
            });
            await context.SaveChangesAsync();

            var dto = new UserDto
            {
                Email = "user@test.com",
                Password = "WrongPassword"
            };

            await Assert.ThrowsAsync<UnauthorizedAccessException>(() => service.Login(dto));
        }

        [Theory]
        [InlineData("", "")]
        [InlineData(null, "")]
        [InlineData("", null)]
        [InlineData(null, null)]
        [InlineData(null, "12345678")]
        [InlineData("", "12345678")]
        [InlineData("test@test.com", null)]
        [InlineData("test@test.com", "")]
        [InlineData("test@test.com", "1234567")]
        [InlineData("test@test.com",
            "000000000000000000000000000000000000000000000000000")]
        public async Task Register_ShouldThrowException_WhenDataISBad(
            string email, string password)
        {
            var service = GetService(out var context, out var jwtConfig);

            UserDto dto = null;

            if (email != null || password != null)
            {
                dto = new UserDto { Email = email, Password = password };
            }

            var exception = await
                Assert.ThrowsAnyAsync<ArgumentException>(() =>
                    service.Register(dto));

            Assert.False(string.IsNullOrWhiteSpace(exception.Message));
        }
        
        [Theory]
        [InlineData("","")]
        [InlineData(null, "")]
        [InlineData("",null)]
        [InlineData(null, null)]
        [InlineData(null,"12345678")]
        [InlineData("","12345678")]
        [InlineData("test@test.com", null)]
        [InlineData("test@test.com", "")]
        [InlineData("test@test.com", "1234567")]
        [InlineData("test@test.com", "000000000000000000000000000000000000000000000000000")]
        public async Task Login_ShouldThrowException_WhenDataIsBad(
            string email, string password)
        {
            var service = GetService(out var context, out var jwtConfig);
            
            UserDto dto = new UserDto { Email = email, Password = password };

            var exception = await Assert.ThrowsAnyAsync<ArgumentException>(() => service.Login(dto));

            Assert.False(string.IsNullOrWhiteSpace(exception.Message));
        }
    }
}
