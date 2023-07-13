using Banks.Classes;
using Banks.Entities.BankAccounts;

namespace Banks.Entities.Transaction.Commands;

public class WithdrawCommand : ICommand
{
    private readonly IBankAccount _account;
    private readonly MoneyAmount _amount;

    public WithdrawCommand(IBankAccount account, MoneyAmount amount)
    {
        _account = account;
        _amount = amount;
    }

    public void Execute()
    {
        _account.Withdraw(_amount);
    }

    public void Cancel()
    {
        _account.PayIn(_amount);
    }
}