using System.ComponentModel.DataAnnotations;
using Pre_Trainee_Task.Models;

namespace Pre_Trainee_Task.DTOs;

public class FeedbackCreateDto
{
    [Required] 
    [MinLength(1)] 
    [MaxLength(50)] 
    public string Title { get; set; } = string.Empty;

    [Required]
    [MinLength(1)]
    [MaxLength(1000)]
    public string Message { get; set; } = string.Empty;

    [Required] 
    [EnumDataType(typeof(FeedbackStatus))]
    public FeedbackStatus Status { get; set; }

    [Required] 
    [EnumDataType(typeof(FeedbackType))]
    public FeedbackType Type { get; set; }

    // [Required] public Guid UserId { get; set; }

    public override string ToString()
    {
        var str = $"""
                   Title: {Title}
                   Message: {Message}
                   Status: {Status}
                   Type: {Type}
                   """;
        return str;
    }
}
