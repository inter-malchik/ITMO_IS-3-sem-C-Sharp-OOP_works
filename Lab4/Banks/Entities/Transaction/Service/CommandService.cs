using Banks.Entities.Transaction.Commands;
using Banks.Entities.Transaction.History;
using Banks.Entities.Transaction.Info;

namespace Banks.Entities.Transaction.Service;

public class CommandService
{
    private readonly CommandHistory _commandHistory = new ();

    public ICommandInfo PerformCommand(ICommand command)
    {
        ICommandInfo info = new CommandInfo(command);

        info.Perform();

        _commandHistory.Push(info);

        return info;
    }

    public void CancelCommand(Guid id)
    {
        ICommandInfo info = _commandHistory.Find(id) ?? throw new NotImplementedException();

        info.Cancel();
    }
}