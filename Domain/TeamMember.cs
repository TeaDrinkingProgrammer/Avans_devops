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

    public void Notify(string message)
    {
        _notifier.Notify(new TeamMemberNotification(this, message));
    }

    public IDisposable Subscribe(IObserver<TeamMemberNotification> observer)
    {
        return _notifier.Subscribe(observer);
    }
}