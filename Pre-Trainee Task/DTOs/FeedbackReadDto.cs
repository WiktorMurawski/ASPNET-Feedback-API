using Pre_Trainee_Task.Models;

namespace Pre_Trainee_Task.DTOs;

public class FeedbackReadDto
{
    public Guid Id { get; init; }
    public string Title { get; set; } = string.Empty;
    public string Message { get; set; } = string.Empty;
    public FeedbackStatus Status { get; set; }
    public FeedbackType Type { get; set; }
    public DateTime CreatedAt { get; set; }
    public Guid UserId { get; set; }
}
