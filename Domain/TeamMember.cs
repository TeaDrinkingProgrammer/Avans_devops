namespace Domain;

public class TeamMember
{
    public readonly string Name;
    private readonly TeamMemberNotifier _notifier;

    public TeamMember(string name)
    {
        Name = name;
        _notifier = new TeamMemberNotifier();
    }

    public void Notify(TeamMemberNotification notification)
    {
        _notifier.Notify(notification);
    }

    public IDisposable Subscribe(IObserver<TeamMemberNotification> observer)
    {
        return _notifier.Subscribe(observer);
    }
}