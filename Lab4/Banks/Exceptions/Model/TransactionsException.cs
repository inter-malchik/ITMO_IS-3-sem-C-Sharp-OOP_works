using Banks.Entities.BankAccounts;
using Banks.Entities.Transaction.Info;

namespace Banks.Exceptions.Model;

public class TransactionsException : BanksBaseException
{
    private TransactionsException(string message)
        : base(message)
    {
    }

    public static TransactionsException AlreadyPerformed(CommandInfo commandInfo)
    {
        return new TransactionsException($"command ({commandInfo.Id}) already performed");
    }

    public static TransactionsException HasNotBeenPerformed(CommandInfo commandInfo)
    {
        return new TransactionsException($"command ({commandInfo.Id}) has not been performed");
    }

    public static TransactionsException AlreadyCanceled(CommandInfo commandInfo)
    {
        return new TransactionsException($"command ({commandInfo.Id}) already canceled");
    }

    public static TransactionsException TransferToTheSameAccount(IBankAccount account)
    {
        return new TransactionsException($"transfering to the same account ({account.Id})");
    }
}