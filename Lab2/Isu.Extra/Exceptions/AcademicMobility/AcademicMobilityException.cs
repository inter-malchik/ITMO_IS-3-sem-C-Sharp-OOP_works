using Isu.Extra.Entities.AcademicMobility;
using Isu.Extra.Entities.People;

namespace Isu.Extra.Exceptions.AcademicMobility;

public class AcademicMobilityException : IsuExtraBaseException
{
    private AcademicMobilityException(string message)
        : base(message)
    { }

    public static AcademicMobilityException
        AlreadyFullyEnrolled(AcademicMobilityStudent student)
    {
        return new AcademicMobilityException($"student {student.WrappedStudent.Name} ({student.Id}) already has two academic mobility courses");
    }

    public static AcademicMobilityException
        NotEnrolled(AcademicMobilityStudent student)
    {
        return new AcademicMobilityException($"student {student.WrappedStudent.Name} ({student.Id}) does not have academic mobility courses");
    }

    public static AcademicMobilityException
        OwnFacultyExtraStudy(AcademicMobilityStudent student)
    {
        return new AcademicMobilityException($"student {student.WrappedStudent.Name} ({student.Id}) can't register at his own megafaculty extra study");
    }

    public static AcademicMobilityException
        NotEnoughPlaces(AcademicMobilityStream stream)
    {
        return new AcademicMobilityException($"stream {stream.Id} has no places for registration");
    }

    public static AcademicMobilityException
        NotEnoughPlaces(AcademicMobilityGroup group)
    {
        return new AcademicMobilityException($"group {group.Id} has no places for registration");
    }

    public static AcademicMobilityException
        NotInExtraStudyGroup(AcademicMobilityGroup group, AcademicMobilityStudent student)
    {
        return new AcademicMobilityException($"student {student.WrappedStudent.Name} ({student.Id}) not in group {group.Id}");
    }
}