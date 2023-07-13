using Banks.Entities.Transaction.Commands;
using Banks.Exceptions.Model;

namespace Banks.Entities.Transaction.Info;

public class CommandInfo : ICommandInfo
{
    public CommandInfo(ICommand command)
    {
        Command = command;
        IsDone = IsCanceled = false;
        Id = Guid.NewGuid();
    }

    public ICommand Command { get; }

    public Guid Id { get; }

    public bool IsDone { get; private set; }

    public bool IsCanceled { get; private set; }

    public void Perform()
    {
        if (IsDone)
        {
            throw TransactionsException.AlreadyPerformed(this);
        }

        Command.Execute();

        IsDone = true;
    }

    public void Cancel()
    {
        if (!IsDone)
        {
            throw TransactionsException.HasNotBeenPerformed(this);
        }

        if (IsCanceled)
        {
            throw TransactionsException.AlreadyCanceled(this);
        }

        Command.Cancel();

        IsCanceled = true;
    }
}