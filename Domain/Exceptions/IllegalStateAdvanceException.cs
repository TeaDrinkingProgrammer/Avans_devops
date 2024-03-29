using System.Runtime.Serialization;

namespace Domain.Exceptions;

[Serializable]
public class IllegalStateAdvanceException : Exception
{
    public IllegalStateAdvanceException()
    {
    }

    public IllegalStateAdvanceException(string message)
        : base(message)
    {
    }

    public IllegalStateAdvanceException(string message, Exception inner)
        : base(message, inner)
    {
    }
    
    protected IllegalStateAdvanceException(SerializationInfo info, StreamingContext context) : base(info, context)
    {

    }
}