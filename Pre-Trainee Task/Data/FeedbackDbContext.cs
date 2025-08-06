using Microsoft.EntityFrameworkCore;
using Pre_Trainee_Task.Models;

namespace Pre_Trainee_Task.Data;

public class FeedbackDbContext : DbContext
{
    public FeedbackDbContext(DbContextOptions<FeedbackDbContext> options) :
        base(options)
    {
    }

    public DbSet<Feedback> Feedbacks { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<AuditLogEntry> AuditLogs { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Test Users
        modelBuilder.Entity<User>().HasData(
            new User
            {
                Id = Guid.NewGuid(),
                Email = "admin@admin.com",
                PasswordHash =
                    BCrypt.Net.BCrypt
                        .HashPassword("admin"), // very bad, only for testing
                Role = UserRole.Admin
            },
            new User
            {
                Id = Guid.NewGuid(),
                Email = "user@user.com",
                PasswordHash = BCrypt.Net.BCrypt.HashPassword("1234"),
                Role = UserRole.User
            });

        // Test Feedbacks
        modelBuilder.Entity<Feedback>().HasData(
            new Feedback
            {
                Id = Guid.NewGuid(),
                Title = "Bug Title",
                Message = "Bug description message",
                CreatedAt = DateTime.UtcNow,
                Status = FeedbackStatus.New,
                Type = FeedbackType.Bug,
                UserId = Guid.NewGuid()
            },
            new Feedback
            {
                Id = Guid.NewGuid(),
                Title = "Suggestion Title",
                Message = "Suggestion description message",
                CreatedAt = DateTime.Today - TimeSpan.FromDays(1),
                Status = FeedbackStatus.InProgress,
                Type = FeedbackType.Suggestion,
                UserId = Guid.NewGuid()
            },
            new Feedback
            {
                Id = Guid.NewGuid(),
                Title = "Question Title",
                Message = "Question message",
                CreatedAt = DateTime.UtcNow - TimeSpan.FromDays(7),
                Status = FeedbackStatus.Closed,
                Type = FeedbackType.Question,
                UserId = Guid.NewGuid()
            }
        );
    }
}
