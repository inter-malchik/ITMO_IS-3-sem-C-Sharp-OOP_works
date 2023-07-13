using Isu.Entities;

namespace Isu.Exceptions;

public class GroupIsNotTrackedException : IsuBaseException
{
    public GroupIsNotTrackedException(Group group)
        : base($"Group {group} is not tracked in service")
    { }
}