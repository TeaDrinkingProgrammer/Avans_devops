namespace Domain.Forum;

public abstract class Post
{
    public string Content { get; }
    public TeamMember Author { get; }

    public Post(string content, TeamMember author)
    {
        Content = content;
        Author = author;
    }
}