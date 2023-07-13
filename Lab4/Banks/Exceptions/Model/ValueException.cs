namespace Banks.Exceptions.Model;

public class ValueException : BanksBaseException
{
    private ValueException(string message)
        : base(message)
    {
    }

    public static ValueException NegativeMoneyAmount(decimal amount)
    {
        return new ValueException($"negative money amount: {amount}");
    }

    public static ValueException IncorrectRateValue(decimal rate)
    {
        return new ValueException($"incorrect rate value: {rate} - not in [0; 1] or [0; 100]");
    }
}