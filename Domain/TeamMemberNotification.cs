namespace Domain;

public class TeamMemberNotification
{
    public readonly TeamMember TeamMember;
    public readonly string Message;

    public TeamMemberNotification(TeamMember teamMember, string message)
    {
        TeamMember = teamMember;
        Message = message;
    }
}