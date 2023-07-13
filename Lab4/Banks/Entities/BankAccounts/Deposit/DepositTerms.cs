using System.Data;
using Banks.Classes;
using Banks.Exceptions.Model;

namespace Banks.Entities.BankAccounts.Deposit;

public class DepositTerms
{
    private DepositTerms(SortedDictionary<MoneyAmount, PercentRate> balanceInterests, TimeSpan timePeriod)
    {
        BalanceInterests = balanceInterests;
        TimePeriod = timePeriod;
    }

    public static DepositTermsBuilder Builder => new DepositTermsBuilder();

    public IReadOnlyDictionary<MoneyAmount, PercentRate> BalanceInterests { get; private set; }

    public TimeSpan TimePeriod { get; }

    public void Update(DepositTerms other)
    {
        BalanceInterests = other.BalanceInterests;
    }

    public class DepositTermsBuilder
    {
        private readonly SortedDictionary<MoneyAmount, PercentRate> _balanceInterest;
        private TimeSpan? _timeSpan;

        public DepositTermsBuilder()
        {
            _balanceInterest = new SortedDictionary<MoneyAmount, PercentRate>();
        }

        public DepositTermsBuilder AddInterestBound(MoneyAmount interestBound, PercentRate interestRate)
        {
            _balanceInterest[interestBound] = interestRate;
            return this;
        }

        public DepositTermsBuilder SetTimeSpan(TimeSpan span)
        {
            _timeSpan = span;
            return this;
        }

        public DepositTerms Build()
        {
            return new DepositTerms(_balanceInterest, _timeSpan ?? throw DepositException.TimeSpanNotSpecified());
        }
    }
}