using Isu.Models;

namespace Isu.Exceptions;

public class GroupWithThisNameAlreadyExistsException : IsuBaseException
{
    public GroupWithThisNameAlreadyExistsException(GroupName groupName)
        : base($"Group {groupName} already present in service")
    { }
}