using Banks.Classes;
using Banks.Entities.Banks;
using Banks.Entities.Clients;
using Banks.Exceptions.Model;

namespace Banks.Entities.BankAccounts.Debit;

public class DebitAccount : IBankAccount
{
    private readonly List<decimal> _interestCalculations;

    public DebitAccount(DebitTerms terms, IBankClient client, IBank bank)
    {
        Terms = terms;
        _interestCalculations = new List<decimal>();
        Id = Guid.NewGuid();
        Client = client;
        Bank = bank;
        Bank.CentralBank.TimeService.Subscribe(this);
    }

    public DebitTerms Terms { get; }

    public MoneyAmount AvailableMoney { get; private set; }

    public Guid Id { get; }

    public IBankClient Client { get; }

    public IBank Bank { get; }

    public void PayIn(MoneyAmount amount)
    {
        AvailableMoney += amount;
    }

    public void Withdraw(MoneyAmount amount)
    {
        if (amount > AvailableMoney)
        {
            throw AccountException.NotEnoughMoneyException(amount, AvailableMoney);
        }

        AvailableMoney -= amount;
    }

    public void HandleNextDay()
    {
        _interestCalculations.Add(Terms.BalanceInterest.Rate / 365 * AvailableMoney.Amount);

        if (_interestCalculations.Count == 30)
        {
            var monthPercents = new MoneyAmount(_interestCalculations.Sum());

            AvailableMoney += monthPercents;
        }
    }

    public void SkipTimePeriod(TimeSpan timeSpan)
    {
        for (int day = 0; day < timeSpan.Days; day++)
        {
            HandleNextDay();
        }
    }
}