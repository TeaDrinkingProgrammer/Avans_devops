namespace Domain.Notifier;

public interface MessagingService
{
    public void Send(string receiver, string message);
}