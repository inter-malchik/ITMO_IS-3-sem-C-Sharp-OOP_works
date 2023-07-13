namespace Banks.Console.Utils;

public static class Writing
{
    public static void ImportantMessage(string message)
    {
        System.Console.WriteLine("================================");
        System.Console.WriteLine(message);
        System.Console.WriteLine("================================");
    }

    public static void SkipLines(int amount = 1)
    {
        for (int i = 0; i < amount; i++)
        {
            System.Console.WriteLine();
        }
    }
}