namespace Domain.Forum;

public abstract class Post
{
    public string Content { get; }
    private TeamMember _author { get; }

    public Post(string content, TeamMember author)
    {
        Content = content;
        _author = author;
    }
}