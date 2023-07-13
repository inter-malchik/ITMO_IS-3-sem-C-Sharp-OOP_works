namespace Banks.Console.Chains;

public interface IHandler
{
    public void Handle(Command command, ProgramState state);

    public IHandler SetNext(IHandler nextHandler);
}