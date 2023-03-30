namespace Domain.Notifier.Events;

public class Notification
{
    public readonly TeamMember TeamMember;
    public readonly string Message;
    public readonly string Type;

    public Notification(TeamMember teamMember, string message, string type)
    {
        Message = message;
        TeamMember = teamMember;
        Type = type;
    }
}