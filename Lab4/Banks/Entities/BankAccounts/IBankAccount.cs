using Banks.Classes;
using Banks.Entities.Banks;
using Banks.Entities.Clients;
using Banks.Time;

namespace Banks.Entities.BankAccounts;

public interface IBankAccount : ITimeSensitive
{
    public MoneyAmount AvailableMoney { get; }

    Guid Id { get; }

    IBankClient Client { get; }

    IBank Bank { get; }

    void PayIn(MoneyAmount amount);

    void Withdraw(MoneyAmount amount);
}