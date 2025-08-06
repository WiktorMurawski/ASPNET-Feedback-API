using System.ComponentModel.DataAnnotations;

namespace Pre_Trainee_Task.Models;

public class AuditLogEntry
{
    [Key] public Guid Id { get; set; } = Guid.NewGuid();

    [Required] public Guid FeedbackId { get; set; }

    [Required] public Method Method { get; set; }

    [Required]
    [MaxLength(100)]
    public string Actor { get; set; } = string.Empty;

    public DateTime Timestamp { get; set; } = DateTime.UtcNow;
}
