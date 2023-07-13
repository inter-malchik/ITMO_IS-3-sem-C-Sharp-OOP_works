using Banks.Classes;
using Banks.Entities.BankAccounts.Deposit;

namespace Banks.Exceptions.Model;

public class DepositException : BanksBaseException
{
    private DepositException(string message)
        : base(message)
    {
    }

    public static DepositException DepositHasNotExpired(DepositAccount deposit)
    {
        return new DepositException($"deposit has not expired (date: {deposit.ClosingDate})");
    }

    public static DepositException TimeSpanNotSpecified()
    {
        return new DepositException($"time span not specified");
    }
}