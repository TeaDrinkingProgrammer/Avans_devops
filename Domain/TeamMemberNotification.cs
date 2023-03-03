namespace Domain;

public class TeamMemberNotification
{
    public readonly TeamMember TeamMember;
    public readonly String Message;

    public TeamMemberNotification(TeamMember teamMember, String message)
    {
        TeamMember = teamMember;
        Message = message;
    }
}