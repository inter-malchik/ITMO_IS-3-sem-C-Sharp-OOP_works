using Banks.Classes;
using Banks.Entities.BankAccounts;
using Banks.Entities.BankAccounts.Credit;
using Banks.Entities.BankAccounts.Debit;
using Banks.Entities.BankAccounts.Deposit;
using Banks.Entities.CentralBanks;
using Banks.Entities.Clients;
using Banks.Entities.Transaction.Info;

namespace Banks.Entities.Banks;

public interface IBank
{
    MoneyAmount DoubtfulClientOperationLimit { get; }

    ICentralBank CentralBank { get; }

    Guid Id { get; }

    void RegisterClient(Client client);

    Client GetClient(Guid id);

    CreditAccount OpenCredit(Client client);

    DepositAccount OpenDeposit(Client client);

    DebitAccount OpenDebit(Client client);

    ICommandInfo PayInMoney(IBankAccount account, MoneyAmount amount);

    ICommandInfo WithdrawMoney(IBankAccount account, MoneyAmount amount);

    ICommandInfo DomesticTransfer(IBankAccount accountFrom, IBankAccount accountTo, MoneyAmount amount);

    void CancelTransaction(Guid id);

    void UpdateCreditTerms(CreditTerms newTerms);

    void UpdateDebitTerms(DebitTerms newTerms);

    void UpdateDepositTerms(DepositTerms newTerms);

    void Subscribe(Client client);

    void UnSubscribe(Client client);
}