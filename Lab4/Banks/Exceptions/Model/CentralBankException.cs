using Banks.Classes;
using Banks.Entities.BankAccounts;
using Banks.Entities.Banks;
using Banks.Entities.Clients;

namespace Banks.Exceptions.Model;

public class CentralBankException : BanksBaseException
{
    private CentralBankException(string message)
        : base(message)
    { }

    public static CentralBankException BankNotRegistered(IBank bank)
    {
        return new CentralBankException($"bank ({bank.Id}) is not registered");
    }

    public static CentralBankException BankNotRegistered(Guid id)
    {
        return new CentralBankException($"bank ({id}) is not registered");
    }

    public static CentralBankException BankAlreadyRegistered(IBank bank)
    {
        return new CentralBankException($"bank ({bank.Id}) has already registered");
    }

    public static CentralBankException TryingToDoDomesticTransfer(IBankAccount from, IBankAccount to)
    {
        return new CentralBankException($"accounts ({from.Id}) and ({to.Id}) are from the same bank ({from.Bank})\n" +
                                        $"consider using domestic transfer service");
    }
}