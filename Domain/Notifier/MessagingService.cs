namespace Domain.Notifier;

public interface IMessagingService
{
    public void Send(string receiver, string message);
}