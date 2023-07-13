using Isu.Extra.Entities.Megafaculties;
using Isu.Extra.Entities.Subjects;

namespace Isu.Extra.Exceptions.Models;

public class MegafacultyException : IsuExtraBaseException
{
    private MegafacultyException(string message)
        : base(message)
    { }

    public static MegafacultyException
        AlreadyHasCourse(Megafaculty faculty, Subject course)
    {
        return new MegafacultyException($"faculty {faculty.Name} ({faculty.Id}) already has course {course.CourseName}");
    }

    public static MegafacultyException
        DontHaveCourse(Megafaculty faculty, Subject course)
    {
        return new MegafacultyException($"faculty {faculty.Name} ({faculty.Id}) don't have course {course.CourseName}");
    }

    public static MegafacultyException
        ParsingException(char facultyLetter)
    {
        return new MegafacultyException($"faculty letter '{facultyLetter}' does not have corresponding megafaculty");
    }
}