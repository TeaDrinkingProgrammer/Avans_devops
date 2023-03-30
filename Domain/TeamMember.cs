namespace Domain;

public class TeamMember
{
    public readonly string Name;
    public readonly string? Email;
    public readonly string? SlackHandle;

    public TeamMember(string name, string? email = null, string? slackHandle = null)
    {
        Name = name;
        Email = email;
        SlackHandle = slackHandle;
    }
}