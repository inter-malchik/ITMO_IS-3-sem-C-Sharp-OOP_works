using Banks.Exceptions.Model;

namespace Banks.Classes;

public struct PercentRate : IComparable
{
    public PercentRate()
    {
        Rate = decimal.Zero;
    }

    public PercentRate(decimal rate)
    {
        if (rate is < 0 or > 100)
        {
            throw ValueException.IncorrectRateValue(rate);
        }

        Rate = (rate > 1) ? rate / 100 : rate;
    }

    public static PercentRate Zero => new ();

    public decimal Rate { get; }

    public new string ToString() => $"{decimal.Round(Rate * 100, 2)} %";

    public int CompareTo(object? obj)
    {
        ArgumentNullException.ThrowIfNull(obj);

        var other = (PercentRate)obj;
        return Rate.CompareTo(other.Rate);
    }
}