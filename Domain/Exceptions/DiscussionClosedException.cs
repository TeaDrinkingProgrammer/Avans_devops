using System.Runtime.Serialization;

namespace Domain.Exceptions;

[Serializable]
public class DiscussionClosedException : Exception
{
    public DiscussionClosedException()
    {
    }

    public DiscussionClosedException(string message)
        : base(message)
    {
    }

    public DiscussionClosedException(string message, Exception inner)
        : base(message, inner)
    {
    }
    
    protected DiscussionClosedException(SerializationInfo info, StreamingContext context) : base(info, context)
    {

    }
}