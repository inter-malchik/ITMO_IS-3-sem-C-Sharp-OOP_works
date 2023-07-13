using Isu.Exceptions;

namespace Isu.Models;

public struct CourseNumber
{
    public CourseNumber(int courseNumber)
    {
        ValidateNumber(courseNumber);
        Number = courseNumber;
    }

    public int Number { get; }

    public static CourseNumber ParseCourseNumber(string groupName)
    {
        return new CourseNumber(int.Parse(groupName[2].ToString()));
    }

    private static void ValidateNumber(int courseNumber)
    {
        if (courseNumber is < 1 or > 6)
            throw new InvalidCourseNumberException(courseNumber);
    }
}