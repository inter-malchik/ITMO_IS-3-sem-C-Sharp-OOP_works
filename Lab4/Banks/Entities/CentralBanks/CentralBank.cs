using Banks.Classes;
using Banks.Entities.BankAccounts;
using Banks.Entities.Banks;
using Banks.Entities.Transaction.Commands;
using Banks.Entities.Transaction.Info;
using Banks.Entities.Transaction.Service;
using Banks.Exceptions.Model;
using Banks.Time;

namespace Banks.Entities.CentralBanks;

public class CentralBank : ICentralBank
{
    private readonly List<IBank> _banks = new ();
    private readonly CommandService _externalService = new ();

    public CentralBank(TimeService timeService)
    {
        TimeService = timeService;
    }

    public TimeService TimeService { get; }

    public bool IsBankRegistered(IBank bank) => _banks.Contains(bank);

    public void RegisterBank(IBank bank)
    {
        if (_banks.Contains(bank))
        {
            throw CentralBankException.BankAlreadyRegistered(bank);
        }

        _banks.Add(bank);
    }

    public IBank GetBank(Guid id)
    {
        return _banks.FirstOrDefault(bank => bank.Id == id) ?? throw CentralBankException.BankNotRegistered(id);
    }

    public ICommandInfo ExternalTransfer(IBankAccount accountFrom, IBankAccount accountTo, MoneyAmount amount)
    {
        CheckBankRegistration(accountFrom.Bank);

        CheckBankRegistration(accountTo.Bank);

        if (accountFrom.Client.IsDoubtful && accountFrom.Bank.DoubtfulClientOperationLimit > amount)
        {
            throw BankException.DoubtfulLimitExceeded(accountFrom.Client, amount, accountFrom.Bank.DoubtfulClientOperationLimit);
        }

        if (accountFrom.Bank == accountTo.Bank)
        {
            throw CentralBankException.TryingToDoDomesticTransfer(accountFrom, accountTo);
        }

        return _externalService.PerformCommand(new TransferCommand(accountFrom, accountTo, amount));
    }

    public void CancelTransaction(Guid id)
    {
        _externalService.CancelCommand(id);
    }

    private void CheckBankRegistration(IBank bank)
    {
        if (!IsBankRegistered(bank))
        {
            throw CentralBankException.BankNotRegistered(bank);
        }
    }
}