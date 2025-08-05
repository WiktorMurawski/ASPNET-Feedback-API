using Microsoft.EntityFrameworkCore;
using Pre_Trainee_Task.Models;

namespace Pre_Trainee_Task.Data;

public class FeedbackDbContext : DbContext
{
    public DbSet<Feedback> Feedbacks { get; set; }
    
    public FeedbackDbContext(DbContextOptions<FeedbackDbContext> options) :
        base(options)
    {
    }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

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
