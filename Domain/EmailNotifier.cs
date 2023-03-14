namespace Domain;

public class EmailNotifier : IObserver<TeamMemberNotification>
{
    private readonly string _email;
    private readonly IWriter _writer;

    public EmailNotifier(string email, IWriter writer)
    {
        _email = email;
        _writer = writer;
    }
    public void OnCompleted()
    {
        // Not Implemented
    }

    public void OnError(Exception error)
    {
        // Not Implemented
    }

    public void OnNext(TeamMemberNotification notification)
    {
        _writer.WriteLine($"To: {notification.TeamMember.Name} <{_email}>: {notification.Message}");
    }
}