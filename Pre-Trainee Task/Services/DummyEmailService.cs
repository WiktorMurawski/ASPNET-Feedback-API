namespace Pre_Trainee_Task.Services;

public class DummyEmailService : IEmailService
{
    public async Task SendAsync(string to, string title, string content)
    {
        await Task.Run(() =>
            Console.WriteLine($"Email to {to}\n{title}\n{content}"));
    }
}
