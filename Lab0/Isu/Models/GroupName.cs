using Isu.Exceptions;

namespace Isu.Models;

public struct GroupName
{
    public GroupName(string groupFullName)
    {
        ValidateName(groupFullName);
        FullName = groupFullName;
        Program = EducationalProgramParser.Parse(groupFullName);
        CourseNumber = CourseNumber.ParseCourseNumber(groupFullName);
    }

    public string FullName { get; }

    public char FacultyLetter => FullName[0];

    public EducationalProgram Program { get; }

    public CourseNumber CourseNumber { get; }

    public override string ToString()
    {
        return FullName;
    }

    private static void ValidateName(string groupName)
    {
        if (groupName.Length < 3)
            throw new InvalidGroupNameException(groupName);

        if (!char.IsLetter(groupName[0]))
            throw new InvalidGroupNameException(groupName);

        if (!groupName[1..].All(letter => char.IsDigit(letter)))
        {
            throw new InvalidGroupNameException(groupName);
        }
    }
}