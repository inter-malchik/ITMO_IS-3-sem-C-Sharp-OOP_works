using Banks.Classes;
using Banks.Entities.BankAccounts;
using Banks.Entities.Banks;
using Banks.Entities.Transaction.Info;
using Banks.Time;

namespace Banks.Entities.CentralBanks;

public interface ICentralBank
{
    TimeService TimeService { get; }

    void RegisterBank(IBank bank);

    IBank GetBank(Guid id);

    ICommandInfo ExternalTransfer(IBankAccount accountFrom, IBankAccount accountTo, MoneyAmount amount);

    void CancelTransaction(Guid id);
}