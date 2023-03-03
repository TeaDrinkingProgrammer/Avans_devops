namespace Domain;

public class EmailNotifier : IObserver<TeamMemberNotification>
{
    private String Email;
    private IWriter _writer;

    public EmailNotifier(String email, IWriter writer)
    {
        Email = email;
        _writer = writer;
    }
    public void OnCompleted()
    {
    }

    public void OnError(Exception error)
    {
    }

    public void OnNext(TeamMemberNotification notification)
    {
        Console.WriteLine();
        _writer.WriteLine($"To: {notification.TeamMember.Name} <{Email}>: {notification.Message}");
    }
}