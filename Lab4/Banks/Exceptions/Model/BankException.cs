using Banks.Classes;
using Banks.Entities.BankAccounts;
using Banks.Entities.Clients;

namespace Banks.Exceptions.Model;

public class BankException : BanksBaseException
{
    private BankException(string message)
        : base(message)
    { }

    public static BankException СlientNotRegistered(IBankClient client)
    {
        return new BankException($"client {client.Name} {client.Surname} ({client.Id}) is not registered");
    }

    public static BankException СlientNotRegistered(Guid id)
    {
        return new BankException($"client ({id}) is not registered");
    }

    public static BankException СlientAlreadyRegistered(IBankClient client)
    {
        return new BankException($"client {client.Name} {client.Surname} ({client.Id}) has already registered");
    }

    public static BankException СlientAlreadySubscribed(IBankClient client)
    {
        return new BankException($"client {client.Name} {client.Surname} ({client.Id}) is already subscribed");
    }

    public static BankException СlientAlreadyUnsubscribed(IBankClient client)
    {
        return new BankException($"client {client.Name} {client.Surname} ({client.Id}) has already unsubscribed");
    }

    public static BankException AccountNotRegistered(IBankAccount account)
    {
        return new BankException($"account ({account.Id}) is not registered");
    }

    public static BankException DoubtfulLimitExceeded(IBankClient client, MoneyAmount limit, MoneyAmount amount)
    {
        return new BankException($"client {client.Name} {client.Surname} ({client.Id}) can't withdraw {amount} (limit: {limit})");
    }
}