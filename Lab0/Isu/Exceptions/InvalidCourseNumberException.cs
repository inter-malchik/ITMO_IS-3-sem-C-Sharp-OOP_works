namespace Isu.Exceptions;

public class InvalidCourseNumberException : IsuBaseException
{
    public InvalidCourseNumberException(int courseNumber)
        : base($"Invalid course number value: {courseNumber}")
    { }
}