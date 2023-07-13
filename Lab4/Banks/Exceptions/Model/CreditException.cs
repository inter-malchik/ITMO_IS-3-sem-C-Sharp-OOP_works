using Banks.Entities.BankAccounts.Credit;

namespace Banks.Exceptions.Model;

public class CreditException : BanksBaseException
{
    private CreditException(string message)
        : base(message)
    {
    }

    public static CreditException TryingToExceedCreditLimit(CreditAccount account)
    {
        return new CreditException($"trying to withdraw below the credit limit - {account.CreditLimit}");
    }
}