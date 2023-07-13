namespace Banks.Console.Chains.Concrete;

public class HelpHandler : IHandler
{
    private IHandler? _next = null;

    public void Handle(Command command, ProgramState state)
    {
        if (command.Commands[0] == "help")
        {
            System.Console.WriteLine($"create_bank - создать банк");
            System.Console.WriteLine($"create_person - создать клиента");
        }
        else
        {
            GoNext(command, state);
        }
    }

    public IHandler SetNext(IHandler nextHandler)
    {
        _next = nextHandler;
        return _next;
    }

    private void GoNext(Command command, ProgramState state)
    {
        if (_next is not null)
        {
            _next.Handle(command, state);
        }
    }
}