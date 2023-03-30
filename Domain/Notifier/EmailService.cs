namespace Domain.Notifier;

public class EmailService : MessagingService
{
    private readonly IWriter _writer;

    public EmailService(IWriter writer)
    {
        _writer = writer;
    }

    public void Send(string reciever, string message)
    {
        _writer.WriteLine($"To: {reciever}: {message}");
    }
}