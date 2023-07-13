using Shops.Exceptions.Validation;

namespace Shops.Goods;

public record class GoodAmount
{
    private int _amount;

    public Good Good { get; init; }

    public int Amount
    {
        get { return _amount; }
        init { _amount = ValidateAmount(value); }
    }

    private static int ValidateAmount(int amount)
    {
        if (amount < 0)
        {
            throw ValidationException.NegativeAmount(amount);
        }

        return amount;
    }

    public GoodAmount(Good good, int amount)
    {
        Good = good;
        Amount = amount;
    }
}