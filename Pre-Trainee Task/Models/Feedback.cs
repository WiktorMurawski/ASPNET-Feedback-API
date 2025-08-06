using System.ComponentModel.DataAnnotations;

namespace Pre_Trainee_Task.Models;

public class Feedback
{
    [Key] [Required] public Guid Id { get; set; }

    [Required] [MaxLength(50)] public string Title { get; set; } = string.Empty;

    [Required]
    [MaxLength(1000)]
    public string Message { get; set; } = string.Empty;

    [Required] public DateTime CreatedAt { get; set; }

    [Required] public FeedbackStatus Status { get; set; }

    [Required] public FeedbackType Type { get; set; }

    [Required] public Guid UserId { get; set; }

    public override string ToString()
    {
        var str = $"""
                   Id: {Id}
                   Title: {Title}
                   Message: {Message}
                   CreatedAt: {CreatedAt}
                   Status: {Status}
                   Type: {Type}
                   UserId: {UserId}
                   """;
        return str;
    }
}
