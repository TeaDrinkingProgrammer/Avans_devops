namespace Domain.Notifier;

public class SlackService : MessagingService
{
    private readonly IWriter _writer;

    public SlackService(IWriter writer)
    {
        _writer = writer;
    }

    public void Send(string reciever, string message)
    {
        _writer.WriteLine($"Sending slack message to {reciever} with message {message}");
    }
}