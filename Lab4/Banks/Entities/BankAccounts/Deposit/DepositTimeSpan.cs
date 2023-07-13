namespace Banks.Entities.BankAccounts.Deposit;

public static class DepositTimeSpan
{
    public static TimeSpan OneMonth => new TimeSpan(30, 0, 0, 0);

    public static TimeSpan ThreeMonth => new TimeSpan(90, 0, 0, 0);

    public static TimeSpan SixMonth => new TimeSpan(180, 0, 0, 0);

    public static TimeSpan Months(int amount) => new TimeSpan(amount * 30, 0, 0, 0);
}