using Banks.Exceptions.Model;

namespace Banks.Classes;

public struct MoneyAmount : IComparable
{
    public MoneyAmount()
    {
        Amount = decimal.Zero;
    }

    public MoneyAmount(decimal amount)
    {
        if (amount < 0)
        {
            throw ValueException.NegativeMoneyAmount(amount);
        }

        Amount = amount;
    }

    public static MoneyAmount Zero => new ();

    public decimal Amount { get; }

    public static MoneyAmount operator +(MoneyAmount a, MoneyAmount b)
        => new (a.Amount + b.Amount);

    public static MoneyAmount operator -(MoneyAmount a, MoneyAmount b)
        => new (a.Amount - b.Amount);

    public static bool operator >(MoneyAmount a, MoneyAmount b)
        => a.Amount > b.Amount;

    public static bool operator <(MoneyAmount a, MoneyAmount b)
        => a.Amount < b.Amount;

    public static bool operator >=(MoneyAmount a, MoneyAmount b)
        => !(a.Amount < b.Amount);

    public static bool operator <=(MoneyAmount a, MoneyAmount b)
        => !(a.Amount > b.Amount);

    public new string ToString() => $"{decimal.Round(Amount, 2)} (y.e.)";

    public int CompareTo(object? obj)
    {
        ArgumentNullException.ThrowIfNull(obj);

        var other = (MoneyAmount)obj;
        return Amount.CompareTo(other.Amount);
    }
}