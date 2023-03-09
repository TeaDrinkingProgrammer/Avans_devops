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

    public void Subscribe(IObserver<TeamMemberNotification> observer)
    {
        _notifier.Subscribe(observer);
    }
}