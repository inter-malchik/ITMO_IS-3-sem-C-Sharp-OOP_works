namespace Isu.Extra.Utils;

public record struct Amount
{
    public int Value { get; private set; }

    public Amount(int value)
    {
        ValidateAmount(value);
        Value = value;
    }

    public static Amount operator +(Amount a) => a;

    public static Amount operator +(Amount a, Amount b) => new Amount(a.Value + b.Value);

    private static void ValidateAmount(int amountValue)
    {
        if (amountValue <= 0)
            throw new NotImplementedException();
    }
}