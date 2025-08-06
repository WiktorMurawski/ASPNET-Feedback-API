using Pre_Trainee_Task.Data;
using Pre_Trainee_Task.DTOs;
using Pre_Trainee_Task.Models;

namespace Pre_Trainee_Task.Services;

public class FeedbackService : IFeedbackService
{
    private readonly FeedbackDbContext _context;

    public FeedbackService(FeedbackDbContext context)
    {
        _context = context;
    }

    public IEnumerable<FeedbackReadDto> GetAll()
    {
        return _context.Feedbacks.Select(f => new FeedbackReadDto
        {
            Id = f.Id,
            Title = f.Title,
            Message = f.Message,
            Type = f.Type,
            Status = f.Status,
            CreatedAt = f.CreatedAt,
            UserId = f.UserId
        }).ToList();
    }

    public FeedbackReadDto? GetById(Guid id)
    {
        var feedback = _context.Feedbacks.Find(id);
        if (feedback == null) return null;

        return new FeedbackReadDto
        {
            Id = feedback.Id,
            Title = feedback.Title,
            Message = feedback.Message,
            Type = feedback.Type,
            Status = feedback.Status,
            CreatedAt = feedback.CreatedAt,
            UserId = feedback.UserId
        };
    }

    public FeedbackReadDto Create(FeedbackCreateDto dto)
    {
        var feedback = new Feedback
        {
            Id = Guid.NewGuid(),
            Title = dto.Title,
            Message = dto.Message,
            Type = dto.Type,
            Status = dto.Status,
            CreatedAt = DateTime.UtcNow,
            UserId = dto.UserId
        };

        _context.Feedbacks.Add(feedback);
        _context.SaveChanges();

        return new FeedbackReadDto
        {
            Id = feedback.Id,
            Title = feedback.Title,
            Message = feedback.Message,
            Type = feedback.Type,
            Status = feedback.Status,
            CreatedAt = feedback.CreatedAt,
            UserId = feedback.UserId
        };
    }

    public FeedbackReadDto? Update(Guid id, FeedbackCreateDto dto)
    {
        var feedback = _context.Feedbacks.Find(id);
        if (feedback == null) return null;

        feedback.Title = dto.Title;
        feedback.Message = dto.Message;
        feedback.Status = dto.Status;
        feedback.Type = dto.Type;
        feedback.UserId = dto.UserId;

        _context.SaveChanges();

        return new FeedbackReadDto
        {
            Id = feedback.Id,
            Title = feedback.Title,
            Message = feedback.Message,
            CreatedAt = feedback.CreatedAt,
            Status = feedback.Status,
            Type = feedback.Type,
            UserId = feedback.UserId
        };
    }

    public bool Delete(Guid id)
    {
        var feedback = _context.Feedbacks.Find(id);
        if (feedback == null) return false;

        _context.Feedbacks.Remove(feedback);
        _context.SaveChanges();
        return true;
    }
}
