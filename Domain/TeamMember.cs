namespace Domain;

public class TeamMember
{
    public string Name;
    private TeamMemberNotifier _notifier;

    public TeamMember(String name)
    {
        Name = name;
        _notifier = new TeamMemberNotifier(this);
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