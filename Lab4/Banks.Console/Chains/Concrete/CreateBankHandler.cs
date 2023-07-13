using System.Globalization;
using Banks.Classes;
using Banks.Console.Utils;
using Banks.Entities.BankAccounts.Credit;
using Banks.Entities.BankAccounts.Debit;
using Banks.Entities.BankAccounts.Deposit;
using Banks.Entities.Banks;

namespace Banks.Console.Chains.Concrete;

public class CreateBankHandler : IHandler
{
    private IHandler? _next = null;

    public void Handle(Command command, ProgramState state)
    {
        if (command.Commands[0] == "create_bank")
        {
            CreateBank(state);
        }
        else
        {
            GoNext(command, state);
        }
    }

    public IHandler SetNext(IHandler nextHandler)
    {
        _next = nextHandler;
        return _next;
    }

    private void GoNext(Command command, ProgramState state)
    {
        if (_next is not null)
        {
            _next.Handle(command, state);
        }
    }

    private void CreateBank(ProgramState state)
    {
        var builder = Bank.Builder;

        builder.AddCentralBank(state.CentralBank);

        builder.AddDoubtfulClientLimit(new MoneyAmount(Entering.EnterDecimal("DoubtfulClientLimit")));

        builder.AddDepositTerms(EnterDepositTerms());
        builder.AddCreditTerms(EnterCreditTerms());
        builder.AddDebitTerms(EnterDebitTerms());

        Bank bank = builder.Build();

        Writing.ImportantMessage($"bank ({bank.Id}) was created!");

        state.Banks.Add(bank);
    }

    private DebitTerms EnterDebitTerms()
    {
        Writing.ImportantMessage("ENTERING DEBIT TERMS");
        return new DebitTerms(new PercentRate(Entering.EnterDecimal("PercentRate(debit)")));
    }

    private CreditTerms EnterCreditTerms()
    {
        Writing.ImportantMessage("ENTERING CREDIT TERMS");
        return new CreditTerms(
            new MoneyAmount(Entering.EnterDecimal("limitFee")),
            new MoneyAmount(Entering.EnterDecimal("creditLimit")));
    }

    private DepositTerms EnterDepositTerms()
    {
        Writing.ImportantMessage("ENTERING DEPOSIT TERMS");
        var builder = DepositTerms.Builder;

        builder.SetTimeSpan(DepositTimeSpan.Months(Entering.EnterInteger("months")));

        int bound = Entering.EnterInteger("how much percent bounds");

        for (int i = 0; i < bound; i++)
        {
            builder.AddInterestBound(
                new MoneyAmount(Entering.EnterDecimal("interestBound")),
                new PercentRate(Entering.EnterDecimal("percentRate")));
        }

        return builder.Build();
    }
}