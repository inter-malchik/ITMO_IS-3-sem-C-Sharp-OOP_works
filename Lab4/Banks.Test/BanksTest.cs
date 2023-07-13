using Banks.Classes;
using Banks.Entities;
using Banks.Entities.BankAccounts.Deposit;
using Banks.Entities.Clients;
using Banks.Exceptions.Model;

namespace Banks.Test;

using Xunit;

public class BanksTest
{
    [Fact]
    public void ClientCreationTest_CorrectCreation()
    {
        Action testCode = () =>
        {
            Client client = Client.Builder
                .AddFirstName("Suren")
                .AddSurname("Ermolaev")
                .Build();
        };

        Exception? exception = Record.Exception(testCode);

        Assert.Null(exception);
    }

    [Fact]
    public void ClientCreationTest_NullableProperties()
    {
        Client client = Client.Builder
            .AddFirstName("Suren")
            .AddSurname("Ermolaev")
            .Build();

        Assert.NotNull(client.Name);
        Assert.NotNull(client.Surname);

        Assert.Null(client.Address);
        Assert.Null(client.PassportNumber);
    }

    [Fact]
    public void ClientTest_DoubtfulAndNotDoubtful()
    {
        Assert.True(Client.Builder
            .AddFirstName("Suren")
            .AddSurname("Ermolaev")
            .Build().IsDoubtful);

        Assert.False(Client.Builder
            .AddFirstName("Suren")
            .AddSurname("Ermolaev")
            .AddPassport("228")
            .AddAddress("ITMO")
            .Build().IsDoubtful);
    }

    [Fact]
    public void BalancesInterestBuilder_CorrectConstruct()
    {
        Action testCode = () =>
        {
            DepositTerms terms = DepositTerms.Builder
                .AddInterestBound(new MoneyAmount(decimal.Zero), new PercentRate(3))
                .AddInterestBound(new MoneyAmount(50000), new PercentRate(3.5m))
                .AddInterestBound(new MoneyAmount(100000), new PercentRate(4.0m))
                .SetTimeSpan(DepositTimeSpan.SixMonth)
                .Build();
        };

        Exception? exception = Record.Exception(testCode);

        Assert.Null(exception);
    }

    [Fact]
    public void DepositTerms_CorrectInvariant()
    {
        DepositTerms terms = DepositTerms.Builder
            .AddInterestBound(new MoneyAmount(decimal.Zero), new PercentRate(3))
            .AddInterestBound(new MoneyAmount(100000), new PercentRate(4.0m))
            .AddInterestBound(new MoneyAmount(50000), new PercentRate(3.5m))
            .SetTimeSpan(DepositTimeSpan.OneMonth)
            .Build();

        Assert.Equal(3, terms.BalanceInterests.Count);

        List<MoneyAmount> amounts = terms.BalanceInterests.Keys.ToList();

        Assert.True(amounts.Zip(amounts.Skip(1), (curr, next) => curr <= next).All(x => x));
    }
}