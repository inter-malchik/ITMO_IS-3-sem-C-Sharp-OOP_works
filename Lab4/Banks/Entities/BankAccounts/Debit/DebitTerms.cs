using Banks.Classes;

namespace Banks.Entities.BankAccounts.Debit;

public class DebitTerms
{
    public DebitTerms(PercentRate balanceInterest)
    {
        BalanceInterest = balanceInterest;
    }

    public PercentRate BalanceInterest { get; private set; }

    public void Update(DebitTerms other)
    {
        BalanceInterest = other.BalanceInterest;
    }
}