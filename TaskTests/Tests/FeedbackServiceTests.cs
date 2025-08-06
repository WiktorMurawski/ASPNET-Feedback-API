using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Pre_Trainee_Task.Data;
using Pre_Trainee_Task.DTOs;
using Pre_Trainee_Task.Models;
using Pre_Trainee_Task.Services;

namespace Pre_Trainee_Task.Tests;

public class FeedbackServiceTests
{
    private FeedbackService GetService(out FeedbackDbContext context)
    {
        var options = new DbContextOptionsBuilder<FeedbackDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;

        context = new FeedbackDbContext(options);
        return new FeedbackService(context, new AuditService(context),
            new HttpContextAccessor());
    }

    [Fact]
    public void Create_Should_Add_Feedback()
    {
        var service = GetService(out var context);
        var dto = new FeedbackCreateDto
        {
            Title = "Test Title",
            Message = "Test Message",
            Status = FeedbackStatus.InProgress,
            Type = FeedbackType.Bug,
            UserId = Guid.NewGuid()
        };

        var result = service.Create(dto);

        Assert.Equal("Test Title", result.Title);
        Assert.Equal("Test Message", result.Message);
        Assert.Single(context.Feedbacks);
    }

    [Fact]
    public void GetById_Should_Return_Null_If_Not_Found()
    {
        var service = GetService(out _);

        var result = service.GetById(Guid.NewGuid());

        Assert.Null(result);
    }

    [Fact]
    public void Update_Should_Update_Feedback()
    {
        var service = GetService(out var context);

        var id = Guid.NewGuid();
        var oldFeedback = new Feedback
        {
            Id = id,
            Title = "Title Old",
            Message = "Message Old",
            Status = FeedbackStatus.InProgress,
            Type = FeedbackType.Bug,
            CreatedAt = DateTime.UtcNow.AddDays(-1),
            UserId = Guid.NewGuid()
        };
        context.Feedbacks.Add(oldFeedback);
        context.SaveChanges();

        var dto = new FeedbackCreateDto
        {
            Title = "New Title",
            Message = "New Message",
            Status = FeedbackStatus.Closed,
            Type = FeedbackType.Bug,
            UserId = Guid.NewGuid()
        };
        service.Update(id, dto);
        var newFeedback = context.Feedbacks.Find(id);

        Assert.NotNull(newFeedback);
        Assert.Equal(newFeedback.Id, oldFeedback.Id);
        Assert.Equal(newFeedback.Title, dto.Title);
        Assert.Equal(newFeedback.Message, dto.Message);
        Assert.Equal(newFeedback.Status, dto.Status);
        Assert.Equal(newFeedback.CreatedAt, newFeedback.CreatedAt);
        Assert.Equal(newFeedback.UserId, newFeedback.UserId);
    }


    [Fact]
    public void Delete_Should_Return_False_If_Not_Found()
    {
        var service = GetService(out var context);

        var result = service.Delete(Guid.NewGuid());

        Assert.False(result);
    }

    [Fact]
    public void Delete_Should_Delete_Feedback()
    {
        var service = GetService(out var context);

        var id = Guid.NewGuid();
        context.Feedbacks.Add(new Feedback
        {
            Id = id,
            Title = "Title",
            Message = "Message",
            Status = FeedbackStatus.InProgress,
            Type = FeedbackType.Bug,
            CreatedAt = DateTime.UtcNow,
            UserId = Guid.NewGuid()
        });
        context.SaveChanges();

        var result = service.Delete(id);

        Assert.True(result);
        Assert.Null(context.Feedbacks.Find(id));
    }
}
