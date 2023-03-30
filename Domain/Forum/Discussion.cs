using System.Net.NetworkInformation;
using System.Runtime.InteropServices;
using Domain.Exceptions;

namespace Domain.Forum;

public class Discussion
{
    private string _title;
    private bool _isClosed { get; set; }
    public List<Reply> Replies { get; }= new();

    public void Reply(Reply reply)
    {
        if (_isClosed)
        {
            throw new DiscussionClosedException();
        }
        Replies.Add(reply);
        //TODO notify subscribers
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