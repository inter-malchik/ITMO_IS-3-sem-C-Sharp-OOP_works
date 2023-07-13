namespace Isu.Exceptions;

public class InvalidGroupNameException : IsuBaseException
{
    public InvalidGroupNameException(string groupName)
        : base($"Invalid group name: {groupName}")
    { }
}