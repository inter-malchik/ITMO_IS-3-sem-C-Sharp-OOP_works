using Banks.Classes;

namespace Banks.Entities.BankAccounts.Credit;

public class CreditTerms
{
    public CreditTerms(MoneyAmount limitFee, MoneyAmount creditLimit)
    {
        LimitFee = limitFee;
        CreditLimit = creditLimit;
    }

    public MoneyAmount LimitFee { get; private set; }

    public MoneyAmount CreditLimit { get; private set; }

    public void Update(CreditTerms other)
    {
        LimitFee = other.LimitFee;
        CreditLimit = other.CreditLimit;
    }
}