namespace Isu.Extra.Exceptions;

public class IsuExtraBaseException : Exception
{
    public IsuExtraBaseException()
    { }

    public IsuExtraBaseException(string message)
        : base(message)
    { }

    public IsuExtraBaseException(string message, Exception inner)
        : base(message, inner)
    { }
}