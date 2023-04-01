using Domain.Exceptions;

namespace Domain.Forum;

public class Discussion
{
    private readonly string _title;
    private bool _isClosed { get; set; }
    public List<Reply> Replies { get; }= new();

    public Discussion(string title)
    {
        _title = title;
    }
    
    public void Reply(Reply reply)
    {
        if (_isClosed)
        {
            throw new DiscussionClosedException();
        }
        Replies.ForEach(r => r.Author.Notify("A new message has been posted in '"  + _title + "' discussion by '" + reply.Author.Name + "': " + reply.Content));
        Replies.Add(reply);
    }
    public void Close()
    {
        _isClosed = true;
    }

    public void Open()
    {
        _isClosed = false;
    }
}