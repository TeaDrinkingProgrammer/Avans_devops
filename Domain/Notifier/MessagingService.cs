namespace Domain.Notifier;

public interface MessagingService
{
    public void Send(string reciever, string message);
}