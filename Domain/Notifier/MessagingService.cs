namespace Domain.Notifier;

//Pattern used: Adapter
public interface IMessagingService
{
    public void Send(string receiver, string message);
}