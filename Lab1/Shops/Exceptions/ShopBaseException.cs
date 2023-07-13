namespace Shops.Exceptions;

public class ShopBaseException : Exception
{
    public ShopBaseException()
    { }

    public ShopBaseException(string message)
        : base(message)
    { }

    public ShopBaseException(string message, Exception inner)
        : base(message, inner)
    { }
}