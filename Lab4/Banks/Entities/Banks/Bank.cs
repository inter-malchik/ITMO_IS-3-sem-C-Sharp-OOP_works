using Banks.Classes;
using Banks.Entities.BankAccounts;
using Banks.Entities.BankAccounts.Credit;
using Banks.Entities.BankAccounts.Debit;
using Banks.Entities.BankAccounts.Deposit;
using Banks.Entities.CentralBanks;
using Banks.Entities.Clients;
using Banks.Entities.Transaction.Commands;
using Banks.Entities.Transaction.Info;
using Banks.Entities.Transaction.Service;
using Banks.Exceptions.Model;

namespace Banks.Entities.Banks;

public class Bank : IBank
{
    private readonly List<Client> _clients;
    private readonly List<IBankAccount> _accounts;
    private readonly CommandService _domesticService;

    private readonly CreditTerms _creditTerms;
    private readonly DebitTerms _debitTerms;
    private readonly DepositTerms _depositTerms;

    private readonly List<Client> _subscribedClients;

    private Bank(
                MoneyAmount doubtfulClientLimit,
                CreditTerms creditTerms,
                DebitTerms debitTerms,
                DepositTerms depositTerms,
                ICentralBank centralBank)
    {
        DoubtfulClientOperationLimit = doubtfulClientLimit;
        Id = Guid.NewGuid();
        _clients = new List<Client>();
        _accounts = new List<IBankAccount>();
        _domesticService = new CommandService();
        _subscribedClients = new List<Client>();
        _creditTerms = creditTerms;
        _debitTerms = debitTerms;
        _depositTerms = depositTerms;
        CentralBank = centralBank;
        centralBank.RegisterBank(this);
    }

    public static BankBuilder Builder => new BankBuilder();

    public ICentralBank CentralBank { get; }

    public MoneyAmount DoubtfulClientOperationLimit { get; }

    public Guid Id { get; }

    public IReadOnlyList<IBankAccount> PersonAccounts(IBankClient client) => _accounts
        .Where(account => account.Client == client)
        .ToList();

    public bool IsClientRegistered(Client client) => _clients.Contains(client);

    public bool IsAccountRegistered(IBankAccount account) => _accounts.Contains(account);

    public void RegisterClient(Client client)
    {
        if (IsClientRegistered(client))
        {
            throw BankException.СlientAlreadyRegistered(client);
        }

        _clients.Add(client);
    }

    public Client GetClient(Guid id)
    {
        return _clients.FirstOrDefault(client => client.Id == id) ?? throw BankException.СlientNotRegistered(id);
    }

    public CreditAccount OpenCredit(Client client)
    {
        CheckClientRegistration(client);

        var newAccount = new CreditAccount(_creditTerms, client, this);
        _accounts.Add(newAccount);
        return newAccount;
    }

    public DepositAccount OpenDeposit(Client client)
    {
        CheckClientRegistration(client);

        var newAccount = new DepositAccount(_depositTerms, client, this);
        _accounts.Add(newAccount);
        return newAccount;
    }

    public DebitAccount OpenDebit(Client client)
    {
        CheckClientRegistration(client);

        var newAccount = new DebitAccount(_debitTerms, client, this);
        _accounts.Add(newAccount);
        return newAccount;
    }

    public ICommandInfo PayInMoney(IBankAccount account, MoneyAmount amount)
    {
        CheckAccountRegistration(account);

        return _domesticService.PerformCommand(new PayInCommand(account, amount));
    }

    public ICommandInfo WithdrawMoney(IBankAccount account, MoneyAmount amount)
    {
        CheckAccountRegistration(account);

        if (account.Client.IsDoubtful && DoubtfulClientOperationLimit > amount)
        {
            throw BankException.DoubtfulLimitExceeded(account.Client, amount, DoubtfulClientOperationLimit);
        }

        return _domesticService.PerformCommand(new WithdrawCommand(account, amount));
    }

    public ICommandInfo DomesticTransfer(IBankAccount accountFrom, IBankAccount accountTo, MoneyAmount amount)
    {
        CheckAccountRegistration(accountFrom);

        CheckAccountRegistration(accountTo);

        if (accountFrom.Client.IsDoubtful && DoubtfulClientOperationLimit > amount)
        {
            throw BankException.DoubtfulLimitExceeded(accountFrom.Client, amount, DoubtfulClientOperationLimit);
        }

        return _domesticService.PerformCommand(new TransferCommand(accountFrom, accountTo, amount));
    }

    public void CancelTransaction(Guid id)
    {
        _domesticService.CancelCommand(id);
    }

    public void UpdateCreditTerms(CreditTerms newTerms)
    {
        _creditTerms.Update(newTerms);

        foreach (var client in _subscribedClients
                     .Where(client => PersonAccounts(client)
                         .Any(account => account is CreditAccount)))
        {
            client.Notify();
        }
    }

    public void UpdateDebitTerms(DebitTerms newTerms)
    {
        _debitTerms.Update(newTerms);

        foreach (var client in _subscribedClients
                     .Where(client => PersonAccounts(client)
                         .Any(account => account is DebitAccount)))
        {
            client.Notify();
        }
    }

    public void UpdateDepositTerms(DepositTerms newTerms)
    {
        _depositTerms.Update(newTerms);

        foreach (var client in _subscribedClients
                               .Where(client => PersonAccounts(client)
                               .Any(account => account is DepositAccount)))
        {
            client.Notify();
        }
    }

    public void Subscribe(Client client)
    {
        CheckClientRegistration(client);

        if (_subscribedClients.Contains(client))
        {
            throw BankException.СlientAlreadySubscribed(client);
        }

        _subscribedClients.Add(client);
    }

    public void UnSubscribe(Client client)
    {
        CheckClientRegistration(client);

        if (!_subscribedClients.Contains(client))
        {
            throw BankException.СlientAlreadyUnsubscribed(client);
        }

        _subscribedClients.Remove(client);
    }

    private void CheckClientRegistration(Client client)
    {
        if (!IsClientRegistered(client))
        {
            throw BankException.СlientNotRegistered(client);
        }
    }

    private void CheckAccountRegistration(IBankAccount account)
    {
        if (!IsAccountRegistered(account))
        {
            throw BankException.AccountNotRegistered(account);
        }
    }

    public class BankBuilder
    {
        private MoneyAmount? _doubtfulClientLimit;
        private CreditTerms? _creditTerms;
        private DebitTerms? _debitTerms;
        private DepositTerms? _depositTerms;
        private ICentralBank? _centralBank;

        public BankBuilder AddDoubtfulClientLimit(MoneyAmount limit)
        {
            _doubtfulClientLimit = limit;
            return this;
        }

        public BankBuilder AddCreditTerms(CreditTerms creditTerms)
        {
            _creditTerms = creditTerms;
            return this;
        }

        public BankBuilder AddDebitTerms(DebitTerms debitTerms)
        {
            _debitTerms = debitTerms;
            return this;
        }

        public BankBuilder AddDepositTerms(DepositTerms depositTerms)
        {
            _depositTerms = depositTerms;
            return this;
        }

        public BankBuilder AddCentralBank(ICentralBank centralBank)
        {
            _centralBank = centralBank;
            return this;
        }

        public Bank Build()
        {
            return new Bank(
                _doubtfulClientLimit ?? throw BankBuilderException.NoDoubtfulClientLimitSpecified(),
                _creditTerms ?? throw BankBuilderException.NoCreditTermsSpecified(),
                _debitTerms ?? throw BankBuilderException.NoDebitTermsSpecified(),
                _depositTerms ?? throw BankBuilderException.NoDepositTermsSpecified(),
                _centralBank ?? throw BankBuilderException.NoCentralBankSpecified());
        }
    }
}