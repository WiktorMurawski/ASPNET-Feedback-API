namespace Pre_Trainee_Task.Services;

public class DummyEmailService : IEmailService
{
    public void Send(string to, string title, string content)
    {
        Console.WriteLine($"Email to {to}\n{title}\n{content}");
    }
}
