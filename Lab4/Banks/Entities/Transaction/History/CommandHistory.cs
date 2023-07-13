using Banks.Entities.Transaction.Info;

namespace Banks.Entities.Transaction.History;

public class CommandHistory
{
    private readonly List<ICommandInfo> _history = new ();

    public IReadOnlyList<ICommandInfo> GetInfo => _history.AsReadOnly();

    public void Push(ICommandInfo info)
    {
        _history.Add(info);
    }

    public ICommandInfo? Find(Guid id)
    {
        return _history.FirstOrDefault(commandInfo => commandInfo.Id == id);
    }
}