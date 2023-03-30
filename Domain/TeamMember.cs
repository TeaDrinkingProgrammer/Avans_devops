using Domain.Notifier.Events;

namespace Domain;

public class TeamMember
{
    public readonly string Name;
    public readonly string? Email;
    public readonly string? SlackHandle;
    private readonly TeamMemberNotifier _notifier;

    public TeamMember(string name, string? email = null, string? slackHandle = null)
    {
        Name = name;
        Email = email;
        SlackHandle = slackHandle;
        _notifier = new TeamMemberNotifier();
    }
    public void Notify(string message)
    {
        _notifier.Notify(new Notification(this, message));
    }
    public IDisposable Subscribe(IObserver<Notification> observer)
    {
        return _notifier.Subscribe(observer);
    }
}