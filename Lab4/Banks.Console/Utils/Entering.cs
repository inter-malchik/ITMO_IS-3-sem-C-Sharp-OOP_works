using System.Globalization;
using Banks.Classes;

namespace Banks.Console.Utils;

public static class Entering
{
    public static decimal EnterDecimal(string message, decimal ifNotParsed = decimal.Zero)
    {
        System.Console.Write($"{message}: ");

        decimal x;

        var cultureInfo = new CultureInfo("it-IT");

        if (decimal.TryParse(System.Console.ReadLine(), NumberStyles.AllowDecimalPoint, cultureInfo, out x))
            return x;
        else
            return ifNotParsed;
    }

    public static int EnterInteger(string message, int ifNotParsed = 0)
    {
        System.Console.Write($"{message}: ");

        return Convert.ToInt32(System.Console.ReadLine());
    }

    public static string EnterString(string message, string? ifNotParsed = null)
    {
        System.Console.Write($"{message}: ");
        return System.Console.ReadLine() !;
    }
}