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

    private void ValidateInput(FeedbackCreateDto dto)
    {
        if (dto == null)
            throw new ArgumentNullException(nameof(dto),
                "FeedbackCreateDto cannot be null");
        if (string.IsNullOrWhiteSpace(dto.Title))
            throw new ArgumentException("Title cannot be empty",
                nameof(dto.Title));
        if (string.IsNullOrWhiteSpace(dto.Message))
            throw new ArgumentException("Message cannot be empty",
                nameof(dto.Message));
        if (dto.Title.Length > 100)
            throw new ArgumentException(
                "Title must be less than or equal to 100 characters.",
                nameof(dto.Title));
        if (dto.Message.Length > 1000)
            throw new ArgumentException(
                "Message must be less than or equal to 1000 characters.",
                nameof(dto.Message));
        if (!Enum.IsDefined(typeof(FeedbackStatus), dto.Status))
            throw new ArgumentOutOfRangeException(nameof(dto.Status),
                "Invalid status value");
        if (!Enum.IsDefined(typeof(FeedbackType), dto.Type))
            throw new ArgumentOutOfRangeException(nameof(dto.Type),
                "Invalid type value");
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
        ValidateInput(dto);
        
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
        ValidateInput(dto);
        
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
