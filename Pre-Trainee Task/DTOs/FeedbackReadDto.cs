using System.ComponentModel.DataAnnotations;
using Pre_Trainee_Task.Models;

namespace Pre_Trainee_Task.DTOs;

public class FeedbackReadDto
{
    [Key] [Required] public Guid Id { get; init; }

    [Required] [MaxLength(50)] public string Title { get; set; } = string.Empty;

    [Required]
    [MaxLength(1000)]
    public string Message { get; set; } = string.Empty;

    [Required] public FeedbackStatus Status { get; set; }

    [Required] public FeedbackType Type { get; set; }

    [Required] public DateTime CreatedAt { get; set; }

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
