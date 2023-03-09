namespace Domain.Exceptions;

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
    
}