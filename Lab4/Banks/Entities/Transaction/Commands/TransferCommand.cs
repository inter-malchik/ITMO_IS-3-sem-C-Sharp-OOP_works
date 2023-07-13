using Banks.Classes;
using Banks.Entities.BankAccounts;
using Banks.Exceptions.Model;

namespace Banks.Entities.Transaction.Commands;

public class TransferCommand : ICommand
{
    private readonly IBankAccount _accountFrom;
    private readonly IBankAccount _accountTo;
    private readonly MoneyAmount _amount;

    public TransferCommand(IBankAccount accountFrom, IBankAccount accountTo, MoneyAmount amount)
    {
        _accountFrom = accountFrom;
        _accountTo = accountTo;
        _amount = amount;

        if (accountFrom == accountTo)
        {
            throw TransactionsException.TransferToTheSameAccount(accountFrom);
        }
    }

    public void Execute()
    {
        _accountFrom.Withdraw(_amount);
        _accountTo.PayIn(_amount);
    }

    public void Cancel()
    {
        _accountFrom.PayIn(_amount);
        _accountTo.Withdraw(_amount);
    }
}