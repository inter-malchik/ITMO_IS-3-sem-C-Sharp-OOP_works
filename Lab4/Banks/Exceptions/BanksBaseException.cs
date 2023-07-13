namespace Banks.Exceptions;

public class BanksBaseException : Exception
{
    public BanksBaseException()
    {
    }

    public BanksBaseException(string message)
        : base(message)
    {
    }

    public BanksBaseException(string message, Exception inner)
        : base(message, inner)
    {
    }
}