using Pre_Trainee_Task.Models;

namespace Pre_Trainee_Task.DTOs;

public class FeedbackCreateDto
{
    public string Title { get; set; }
    public string Message { get; set; }
    public FeedbackStatus Status { get; set; }
    public FeedbackType Type { get; set; }
    public Guid UserId { get; set; }
}

