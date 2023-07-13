using Isu.Entities;

namespace Isu.Exceptions;

public class StudentAlreadyPresentInGroupException : IsuBaseException
{
    public StudentAlreadyPresentInGroupException(Student student)
        : base($"student {student} already enrolled in {student.Group}")
    { }
}