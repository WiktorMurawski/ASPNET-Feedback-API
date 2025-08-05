namespace Pre_Trainee_Task.Models;

public class Feedback
{
    public Guid Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Message { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
    public FeedbackStatus Status { get; set; }
    public FeedbackType Type { get; set; }
    public Guid UserId { get; set; }
}