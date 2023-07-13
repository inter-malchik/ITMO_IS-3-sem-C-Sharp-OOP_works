using Isu.Entities;

namespace Isu.Exceptions;

public class StudentIsNotPresentInGroupException : IsuBaseException
{
    public StudentIsNotPresentInGroupException(Student student, Group group)
        : base($"student {student} is not in group {group}. His group is {student.Group}")
    { }
}