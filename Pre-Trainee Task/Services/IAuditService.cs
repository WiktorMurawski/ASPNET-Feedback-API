using Pre_Trainee_Task.Models;

namespace Pre_Trainee_Task.Services;

public interface IAuditService
{
    void Log(AuditLogEntry log);
}
