using Banks.Entities.Transaction.Commands;

namespace Banks.Entities.Transaction.Info;

public interface ICommandInfo
{
    public ICommand Command { get; }

    public Guid Id { get; }

    public bool IsDone { get; }

    public bool IsCanceled { get; }

    void Perform();

    void Cancel();
}