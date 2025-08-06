namespace Pre_Trainee_Task.Services;

public interface IEmailService
{
    void Send(string to, string title, string content);
}
