namespace Banks.Console.Chains.Concrete;

public class NoFoundHandler : IHandler
{
    public void Handle(Command command, ProgramState state)
    {
        System.Console.Write($"Couldn't parse ");
        System.Console.WriteLine("[{0}]", string.Join(" __ ", command.Commands));
    }

    public IHandler SetNext(IHandler nextHandler)
    {
        throw new NotImplementedException();
    }
}