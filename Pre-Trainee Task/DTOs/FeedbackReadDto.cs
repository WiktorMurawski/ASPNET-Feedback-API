using System.ComponentModel.DataAnnotations;
using Pre_Trainee_Task.Models;

namespace Pre_Trainee_Task.DTOs;

public class FeedbackReadDto
{
    [Key]
    [Required]
    public Guid Id { get; init; }
    [Required]
    [MaxLength(50)]
    public string Title { get; set; } = string.Empty;
    [Required]
    [MaxLength(1000)]
    public string Message { get; set; } = string.Empty;
    [Required]
    public FeedbackStatus Status { get; set; }
    [Required]
    public FeedbackType Type { get; set; }
    [Required]
    public DateTime CreatedAt { get; set; }
    [Required]
    public Guid UserId { get; set; }
    
    public override string ToString()
    {
        string str = $"""
                      Id: {this.Id}
                      Title: {this.Title}
                      Message: {this.Message}
                      CreatedAt: {this.CreatedAt}
                      Status: {this.Status}
                      Type: {this.Type}
                      UserId: {this.UserId}
                      """;
        return str;
    }
}
