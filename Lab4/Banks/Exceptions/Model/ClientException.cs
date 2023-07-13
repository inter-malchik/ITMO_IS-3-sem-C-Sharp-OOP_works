namespace Banks.Exceptions.Model;

public class ClientException : BanksBaseException
{
    private ClientException(string message)
        : base(message)
    { }

    public static ClientException NoNameSpecified()
    {
        return new ClientException($"no name was specified when constructing client");
    }

    public static ClientException NoSurnameSpecified()
    {
        return new ClientException($"no surname was specified when constructing client");
    }
}