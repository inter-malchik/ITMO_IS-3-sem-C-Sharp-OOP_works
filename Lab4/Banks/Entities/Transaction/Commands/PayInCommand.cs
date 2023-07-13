using Banks.Classes;
using Banks.Entities.BankAccounts;

namespace Banks.Entities.Transaction.Commands;

public class PayInCommand : ICommand
{
    private readonly IBankAccount _account;
    private readonly MoneyAmount _amount;

    public PayInCommand(IBankAccount account, MoneyAmount amount)
    {
        _account = account;
        _amount = amount;
    }

    public void Execute()
    {
        _account.PayIn(_amount);
    }

    public void Cancel()
    {
        _account.Withdraw(_amount);
    }
}