using Shops.Exceptions.Buying;
using Shops.Exceptions.Validation;

namespace Shops.Buyers;

public class Buyer : IEquatable<Buyer>
{
    public Buyer(string name, decimal money = 0)
    {
        Name = name;
        if (money < 0)
        {
            throw ValidationException.NegativeMoney(money);
        }

        Money = money;
        Id = Guid.NewGuid();
    }

    public string Name { get; }

    public decimal Money { get; private set; }

    public Guid Id { get; }

    public void PayInMoney(decimal value)
    {
        if (value < 0)
        {
            throw ValidationException.NegativeMoney(value);
        }

        Money += value;
    }

    public void WithdrawMoney(decimal value)
    {
        if (value < 0)
        {
            throw ValidationException.NegativeMoney(value);
        }

        if (value > Money)
        {
            throw BuyingException.NotEnoughMoney(value, this);
        }

        Money -= value;
    }

    public bool Equals(Buyer? other)
    {
        return other is not null && Id == other.Id;
    }
}
