using Isu.Entities;

namespace Isu.Exceptions;

public class MaxStudentsPerGroupException : IsuBaseException
{
    public MaxStudentsPerGroupException(Group studentsGroup)
        : base($"{studentsGroup} already has maximum amount of students")
    { }
}