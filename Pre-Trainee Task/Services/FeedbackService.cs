using System.Security.Claims;
using Pre_Trainee_Task.Data;
using Pre_Trainee_Task.DTOs;
using Pre_Trainee_Task.Models;

namespace Pre_Trainee_Task.Services;

public class FeedbackService : IFeedbackService
{
    private readonly IAuditService _auditService;
    private readonly FeedbackDbContext _context;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public FeedbackService(
        FeedbackDbContext context,
        IAuditService auditService,
        IHttpContextAccessor httpContextAccessor)
    {
        _context = context;
        _auditService = auditService;
        _httpContextAccessor = httpContextAccessor;
    }

    public IQueryable<FeedbackReadDto> GetAll()
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
        });
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

        MakeAuditLogEntry(feedback.Id, Method.POST);

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

        MakeAuditLogEntry(feedback.Id, Method.PUT);

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

        MakeAuditLogEntry(feedback.Id, Method.DELETE);

        _context.Feedbacks.Remove(feedback);
        _context.SaveChanges();
        return true;
    }

    private void MakeAuditLogEntry(Guid id, Method method)
    {
        var logEntry =  new AuditLogEntry()
        {
            Id = Guid.NewGuid(),
            Actor = GetCurrentUser(),
            FeedbackId = id,
            Method = method,
            Timestamp = DateTime.UtcNow
        };
        _auditService.Log(logEntry);
    }
    
    private string GetCurrentUser()
    {
        var user = _httpContextAccessor.HttpContext?.User;

        var email = user?.FindFirst(ClaimTypes.Email)?.Value;
        if (!string.IsNullOrEmpty(email))
            return email;

        return "Unknown";
    }
}
