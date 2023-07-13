using Banks.Console.Utils;
using Banks.Entities.BankAccounts;

namespace Banks.Console.Chains.Concrete;

public class CreateAccountHandler : IHandler
{
    private IHandler? _next = null;

    public void Handle(Command command, ProgramState state)
    {
        if (command.Commands[0] == "create_account")
        {
            IBankAccount account = CreateAccount(
                                    state,
                                    command.Commands[1],
                                    Guid.Parse(command.Commands[2]),
                                    Guid.Parse(command.Commands[3]));
            Writing.ImportantMessage($"Account ({account.Id}) was created");
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

    private IBankAccount CreateAccount(ProgramState state, string type, Guid bankGuid, Guid personGuid)
    {
        var bank = state.CentralBank.GetBank(bankGuid);
        var client = bank.GetClient(personGuid);

        switch (type)
        {
            case "credit":
                return bank.OpenCredit(client);
            case "debit":
                return bank.OpenDebit(client);
            case "deposit":
                return bank.OpenDeposit(client);
        }

        throw new ArgumentException($"incorrect type - {type}");
    }
}