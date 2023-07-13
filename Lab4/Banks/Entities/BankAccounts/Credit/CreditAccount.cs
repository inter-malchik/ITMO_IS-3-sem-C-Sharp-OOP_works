using Banks.Classes;
using Banks.Entities.Banks;
using Banks.Entities.Clients;
using Banks.Exceptions.Model;
using Banks.Time;

namespace Banks.Entities.BankAccounts.Credit;

public class CreditAccount : IBankAccount
{
    private decimal _balance;

    public CreditAccount(CreditTerms terms, IBankClient client, IBank bank)
    {
        Terms = terms;
        _balance = decimal.Zero;
        Id = Guid.NewGuid();
        Client = client;
        Bank = bank;
    }

    public CreditTerms Terms { get; }

    public MoneyAmount CreditLimit => Terms.CreditLimit;

    public MoneyAmount AvailableMoney => new (CreditLimit.Amount + _balance);

    public Guid Id { get; }

    public IBankClient Client { get; }

    public IBank Bank { get; }

    public void PayIn(MoneyAmount amount)
    {
        _balance += amount.Amount;
    }

    public void Withdraw(MoneyAmount amount)
    {
        if (-amount.Amount < _balance)
        {
            throw CreditException.TryingToExceedCreditLimit(this);
        }

        _balance = _balance - amount.Amount - Terms.LimitFee.Amount;
    }

    public void SkipTimePeriod(TimeSpan timeSpan)
    { }

    // (is not time sensitive)
}