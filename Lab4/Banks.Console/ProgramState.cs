using Banks.Entities.Banks;
using Banks.Entities.CentralBanks;
using Banks.Entities.Clients;
using Banks.Time;

namespace Banks.Console;

public class ProgramState
{
    private CentralBank _centralBank;
    private List<Client> _clients;
    private List<Bank> _banks;

    public ProgramState()
    {
        _centralBank = new CentralBank(new TimeService());
        _clients = new List<Client>();
        _banks = new List<Bank>();
    }

    public CentralBank CentralBank => _centralBank;

    public List<Client> Clients => _clients;

    public List<Bank> Banks => _banks;

    // this object is mutable by definition
}