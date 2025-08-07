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
    public async Task Create_ShouldAddFeedback()
    {
        var service = GetService(out var context);
        var dto = new FeedbackCreateDto
        {
            Title = "Test Title",
            Message = "Test Message",
            Status = FeedbackStatus.InProgress,
            Type = FeedbackType.Bug,
            // UserId = Guid.NewGuid()
        };

        var result = await service.CreateAsync(dto);

        Assert.Equal("Test Title", result.Title);
        Assert.Equal("Test Message", result.Message);
        Assert.Single(context.Feedbacks);
    }
    
    [Fact]
    public async Task GetById_ShouldReturnNull_IfNotFound()
    {
        var service = GetService(out _);

        var result = await service.GetFeedbackByIdAsync(Guid.NewGuid());

        Assert.Null(result);
    }

    [Fact]
    public async Task Update_ShouldUpdateFeedback()
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
        await context.Feedbacks.AddAsync(oldFeedback);
        await context.SaveChangesAsync();

        var dto = new FeedbackCreateDto
        {
            Title = "New Title",
            Message = "New Message",
            Status = FeedbackStatus.Closed,
            Type = FeedbackType.Bug,
        };
        await service.UpdateAsync(id, dto);
        var newFeedback = await context.Feedbacks.FindAsync(id);

        Assert.NotNull(newFeedback);
        Assert.Equal(newFeedback.Id, oldFeedback.Id);
        Assert.Equal(newFeedback.Title, dto.Title);
        Assert.Equal(newFeedback.Message, dto.Message);
        Assert.Equal(newFeedback.Status, dto.Status);
        Assert.Equal(newFeedback.CreatedAt, oldFeedback.CreatedAt);
        Assert.Equal(newFeedback.UserId, oldFeedback.UserId);
    }

    [Fact]
    public async Task DeleteAsync_ShouldReturnFalse_IfNotFound()
    {
        var service = GetService(out _);

        var result = await service.DeleteAsync(Guid.NewGuid());

        Assert.False(result);
    }

    [Fact]
    public async Task DeleteAsync_ShouldDeleteFeedback()
    {
        var service = GetService(out var context);

        var id = Guid.NewGuid();
        await context.Feedbacks.AddAsync(new Feedback
        {
            Id = id,
            Title = "Title",
            Message = "Message",
            Status = FeedbackStatus.InProgress,
            Type = FeedbackType.Bug,
            CreatedAt = DateTime.UtcNow,
            UserId = Guid.NewGuid()
        });
        await context.SaveChangesAsync();

        var result = await service.DeleteAsync(id);

        Assert.True(result);
        Assert.Null(await context.Feedbacks.FindAsync(id));
    }

    [Fact]
    public async Task AuditLogs_ShouldGetCreated()
    {
        var service = GetService(out var context);
        
        // POST
        var dtoPost = new FeedbackCreateDto
        {
            Title = "Test Title",
            Message = "Test Message",
            Status = FeedbackStatus.InProgress,
            Type = FeedbackType.Bug,
            // UserId = Guid.NewGuid()
        };
        var feedbackPost = await service.CreateAsync(dtoPost);
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
        await context.Feedbacks.AddAsync(oldFeedback);
        await context.SaveChangesAsync();
        
        var dtoPut = new FeedbackCreateDto
        {
            Title = "New Title",
            Message = "New Message",
            Status = FeedbackStatus.Closed,
            Type = FeedbackType.Bug,
            // UserId = Guid.NewGuid()
        };
        await service.UpdateAsync(idPut, dtoPut);
        
        // DELETE
        var idDelete = Guid.NewGuid();
        await context.Feedbacks.AddAsync(new Feedback
        {
            Id = idDelete,
            Title = "Title",
            Message = "Message",
            Status = FeedbackStatus.InProgress,
            Type = FeedbackType.Bug,
            CreatedAt = DateTime.UtcNow,
            UserId = Guid.NewGuid()
        });
        await context.SaveChangesAsync();
        await service.DeleteAsync(idDelete);

        var result1 = await context.AuditLogs.FirstOrDefaultAsync(log => 
            log.FeedbackId == idPost);
        var result2 = await context.AuditLogs.FirstOrDefaultAsync(log => 
            log.FeedbackId == idPut);
        var result3 = await context.AuditLogs.FirstOrDefaultAsync(log => 
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
    public async Task Create_ShouldThrowException_WhenDataIsBad(string title, string message, FeedbackStatus status, FeedbackType feedbackType)
    {
        var service = GetService(out _);

        FeedbackCreateDto dto = new FeedbackCreateDto()
        {
            Title = title,
            Message = message,
            Status = status,
            Type = feedbackType,
            // UserId = Guid.NewGuid()
        };
    
        var exception = await Assert.ThrowsAnyAsync<ArgumentException>(() => service.CreateAsync(dto));
    
        Assert.False(string.IsNullOrWhiteSpace(exception.Message));
    }
}
