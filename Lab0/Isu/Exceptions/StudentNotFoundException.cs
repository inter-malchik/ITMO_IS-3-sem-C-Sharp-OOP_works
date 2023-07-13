namespace Isu.Exceptions;

public class StudentNotFoundException : IsuBaseException
{
    public StudentNotFoundException(int stundentId)
        : base($"student with id {stundentId} wasn't found")
    { }
}