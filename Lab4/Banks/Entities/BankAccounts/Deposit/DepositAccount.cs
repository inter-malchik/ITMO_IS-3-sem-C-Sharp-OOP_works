using Banks.Classes;
using Banks.Entities.Banks;
using Banks.Entities.Clients;
using Banks.Exceptions.Model;
using Banks.Time;

namespace Banks.Entities.BankAccounts.Deposit;

public class DepositAccount : IBankAccount
{
    private readonly List<decimal> _interestCalculations;

    public DepositAccount(DepositTerms terms, IBankClient client, IBank bank)
    {
        Terms = terms;
        _interestCalculations = new List<decimal>();
        Id = Guid.NewGuid();
        Client = client;
        Bank = bank;
        Bank.CentralBank.TimeService.Subscribe(this);
        OpeningDate = Bank.CentralBank.TimeService.GetTime;
        ClosingDate = OpeningDate + terms.TimePeriod;
    }

    public DepositTerms Terms { get; }

    public MoneyAmount AvailableMoney { get; private set; }

    public Guid Id { get; }

    public IBankClient Client { get; }

    public IBank Bank { get; }

    public DateTime OpeningDate { get; }

    public DateTime ClosingDate { get; }

    public void PayIn(MoneyAmount amount)
    {
        AvailableMoney += amount;
    }

    public void Withdraw(MoneyAmount amount)
    {
        if (DateTime.Now < ClosingDate)
        {
            throw DepositException.DepositHasNotExpired(this);
        }

        if (amount > AvailableMoney)
        {
            throw AccountException.NotEnoughMoneyException(amount, AvailableMoney);
        }

        AvailableMoney -= amount;
    }

    public void HandleNextDay()
    {
        decimal balanceInterest = Terms.BalanceInterests
            .Where(termPair => termPair.Key.Amount > AvailableMoney.Amount)
            .Max(termPair => termPair.Value.Rate);

        _interestCalculations.Add(balanceInterest / 365 * AvailableMoney.Amount);

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