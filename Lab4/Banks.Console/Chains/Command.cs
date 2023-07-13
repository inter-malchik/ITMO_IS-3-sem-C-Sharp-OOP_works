using System.Text.RegularExpressions;

namespace Banks.Console.Chains;

public struct Command
{
    public Command(string command)
    {
        Commands = Regex.Matches(command, @"\w+")
            .Cast<Match>()
            .Select(x => x.Value)
            .ToArray();
    }

    public string[] Commands { get; }

    public static Command Parse()
    {
        string? line = null;

        while (line is null)
        {
            line = System.Console.ReadLine();
        }

        return new Command(line);
    }
}