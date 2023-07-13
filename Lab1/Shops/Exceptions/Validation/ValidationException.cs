namespace Shops.Exceptions.Validation;

public class ValidationException : ShopBaseException
{
    private ValidationException(string message)
        : base(message)
    { }

    public static ValidationException
        NegativeAmount(int amount)
    {
        return new ValidationException($"value {amount} is not supported in the logic");
    }

    public static ValidationException
        NegativeMoney(decimal moneyAmount)
    {
        return new ValidationException($"value {moneyAmount} is not supported in the logic");
    }
}