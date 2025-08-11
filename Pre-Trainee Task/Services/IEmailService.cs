namespace Pre_Trainee_Task.Services;

public interface IEmailService
{
    Task SendAsync(string to, string title, string content);
}
