using Banks.Console.Chains;
using Banks.Console.Chains.Concrete;

namespace Banks.Console;

public class Program
{
    public static void Main(string[] args)
    {
        var state = new ProgramState();

        IHandler handler = new HelpHandler();
        handler.SetNext(new CreatePersonHandler())
               .SetNext(new CreateBankHandler())
               .SetNext(new ExitHandler())
               .SetNext(new NoFoundHandler());

        while (true)
        {
            handler.Handle(Command.Parse(), state);
        }
    }
}