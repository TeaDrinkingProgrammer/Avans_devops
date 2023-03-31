namespace Domain.Forum;

public class Reply : Post
{
    public Reply(string content, TeamMember teamMember) : base(content, teamMember)
    {
    }
}