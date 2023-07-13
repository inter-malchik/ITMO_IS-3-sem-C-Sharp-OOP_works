namespace Isu.Exceptions;

public class IsuBaseException : Exception
{
    public IsuBaseException()
    { }

    public IsuBaseException(string message)
        : base(message)
    { }

    public IsuBaseException(string message, Exception inner)
        : base(message, inner)
    { }
}