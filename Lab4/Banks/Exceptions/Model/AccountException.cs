using Banks.Classes;

namespace Banks.Exceptions.Model;

public class AccountException : BanksBaseException
{
    private AccountException(string message)
        : base(message)
    {
    }

    public static AccountException NotEnoughMoneyException(MoneyAmount withdrawn, MoneyAmount actual)
    {
        return new AccountException($"trying to withdraw {withdrawn} with {actual}");
    }
}