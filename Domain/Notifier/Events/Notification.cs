namespace Domain.Notifier.Events;

public class Notification
{
    public readonly TeamMember TeamMember;
    public readonly string Message;

    public Notification(TeamMember teamMember, string message)
    {
        Message = message;
        TeamMember = teamMember;
    }
}