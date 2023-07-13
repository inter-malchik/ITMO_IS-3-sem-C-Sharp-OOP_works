using Banks.Console.Utils;
using Banks.Entities.Clients;

namespace Banks.Console.Chains.Concrete;

public class CreatePersonHandler : IHandler
{
    private IHandler? _next = null;

    public void Handle(Command command, ProgramState state)
    {
        if (command.Commands[0] == "create_person")
        {
            CreatePerson(state);
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

    private void CreatePerson(ProgramState state)
    {
        var builder = Client.Builder;

        builder.AddFirstName(Entering.EnterString("FirstName"));
        builder.AddSurname(Entering.EnterString("Surname"));

        builder.AddPassport(Entering.EnterString("Passport (may skip)"));
        builder.AddAddress(Entering.EnterString("Address (may skip)"));

        var client = builder.Build();

        Writing.ImportantMessage($"client ({client.Id}) was created!");

        state.Clients.Add(client);

        foreach (var bank in state.Banks)
        {
            bank.RegisterClient(client);
        }
    }
}