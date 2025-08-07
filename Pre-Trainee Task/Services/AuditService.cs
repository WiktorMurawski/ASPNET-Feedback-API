using Pre_Trainee_Task.Data;
using Pre_Trainee_Task.Models;

namespace Pre_Trainee_Task.Services;

public class AuditService : IAuditService
{
    private readonly FeedbackDbContext _context;

    public AuditService(FeedbackDbContext context)
    {
        _context = context;
    }

    public async Task LogAsync(AuditLogEntry log)
    {
        await _context.AuditLogs.AddAsync(log);
        await _context.SaveChangesAsync();
    }
}
