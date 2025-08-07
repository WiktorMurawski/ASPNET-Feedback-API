using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Pre_Trainee_Task.Data;
using Pre_Trainee_Task.DTOs;
using Pre_Trainee_Task.Models;
using Pre_Trainee_Task.Services;

namespace TestProject.Tests;

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
    public void Create_ShouldAddFeedback()
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
    public void GetById_ShouldReturnNull_IfNotFound()
    {
        var service = GetService(out _);

        var result = service.GetById(Guid.NewGuid());

        Assert.Null(result);
    }

    [Fact]
    public void Update_ShouldUpdateFeedback()
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
        Assert.Equal(newFeedback.CreatedAt, oldFeedback.CreatedAt);
        Assert.Equal(newFeedback.UserId, oldFeedback.UserId);
    }

    [Fact]
    public void Delete_ShouldReturnFalse_IfNotFound()
    {
        var service = GetService(out var context);

        var result = service.Delete(Guid.NewGuid());

        Assert.False(result);
    }

    [Fact]
    public void Delete_ShouldDeleteFeedback()
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

    [Fact]
    public void AuditLogs_ShouldGetCreated()
    {
        var service = GetService(out var context);
        
        // POST
        var dtoPost = new FeedbackCreateDto
        {
            Title = "Test Title",
            Message = "Test Message",
            Status = FeedbackStatus.InProgress,
            Type = FeedbackType.Bug,
            UserId = Guid.NewGuid()
        };
        var feedbackPost = service.Create(dtoPost);
        var idPost = feedbackPost.Id;
        
        // PUT
        var idPut = Guid.NewGuid();
        var oldFeedback = new Feedback
        {
            Id = idPut,
            Title = "Title Old",
            Message = "Message Old",
            Status = FeedbackStatus.InProgress,
            Type = FeedbackType.Bug,
            CreatedAt = DateTime.UtcNow.AddDays(-1),
            UserId = Guid.NewGuid()
        };
        context.Feedbacks.Add(oldFeedback);
        context.SaveChanges();
        
        var dtoPut = new FeedbackCreateDto
        {
            Title = "New Title",
            Message = "New Message",
            Status = FeedbackStatus.Closed,
            Type = FeedbackType.Bug,
            UserId = Guid.NewGuid()
        };
        service.Update(idPut, dtoPut);
        
        // DELETE
        var idDelete = Guid.NewGuid();
        context.Feedbacks.Add(new Feedback
        {
            Id = idDelete,
            Title = "Title",
            Message = "Message",
            Status = FeedbackStatus.InProgress,
            Type = FeedbackType.Bug,
            CreatedAt = DateTime.UtcNow,
            UserId = Guid.NewGuid()
        });
        context.SaveChanges();
        service.Delete(idDelete);

        var result1 = context.AuditLogs.FirstOrDefault(log => 
            log.FeedbackId == idPost);
        var result2 = context.AuditLogs.FirstOrDefault(log => 
            log.FeedbackId == idPut);
        var result3 = context.AuditLogs.FirstOrDefault(log => 
            log.FeedbackId == idDelete);
        
        Assert.NotNull(result1);
        Assert.NotNull(result2);
        Assert.NotNull(result3);
    }

    [Theory]
    [InlineData(null,"message",(FeedbackStatus)0,(FeedbackType)0)]
    [InlineData("","message",(FeedbackStatus)0,(FeedbackType)0)]
    [InlineData("title",null,(FeedbackStatus)0,(FeedbackType)0)]
    [InlineData("title","",(FeedbackStatus)0,(FeedbackType)0)]
    [InlineData("title","message",(FeedbackStatus)4,(FeedbackType)0)]
    [InlineData("title","message",(FeedbackStatus)0,(FeedbackType)4)]
    public void Create_ShouldThrowException_WhenDataIsBad(string title, string message, FeedbackStatus status, FeedbackType feedbackType)
    {
        var service = GetService(out _);

        FeedbackCreateDto dto = new FeedbackCreateDto()
        {
            Title = title,
            Message = message,
            Status = status,
            Type = feedbackType,
            UserId = Guid.NewGuid()
        };
    
        var exception = Assert.ThrowsAny<ArgumentException>(() => service.Create(dto));
    
        Assert.False(string.IsNullOrWhiteSpace(exception.Message));
    }
}
