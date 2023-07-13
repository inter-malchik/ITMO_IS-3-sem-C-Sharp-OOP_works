namespace Banks.Entities.Transaction.Commands;

public interface ICommand
{
    void Execute();

    void Cancel();
}