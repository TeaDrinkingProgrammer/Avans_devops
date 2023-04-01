namespace Domain.Notifier;

public class EmailService : MessagingService
{
    private readonly IWriter _writer;

    public EmailService(IWriter writer)
    {
        _writer = writer;
    }

    public void Send(string receiver, string message)
    {
        _writer.WriteLine($"To: {receiver}: {message}");
    }
}