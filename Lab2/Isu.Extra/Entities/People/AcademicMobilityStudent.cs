using Isu.Entities;
using Isu.Extra.Entities.AcademicMobility;
using Isu.Extra.Entities.Lessons.Schedule;
using Isu.Extra.Exceptions.AcademicMobility;
using Isu.Extra.Utils;

namespace Isu.Extra.Entities.People;

public class AcademicMobilityStudent
{
    public AcademicMobilityStudent(Student wrappedStudent, Schedule mainSchedule)
    {
        WrappedStudent = wrappedStudent;
        Id = GuidGenerator.GenerateId();
        MainSchedule = mainSchedule;
    }

    public Guid Id { get; }

    public Schedule MainSchedule { get; }

    public AcademicMobilityGroup? ExtraStudyGroupMain { get; private set; }

    public AcademicMobilityGroup? ExtraStudyGroupSecondary { get; private set; }

    public bool HasExtraStudies => ExtraStudyGroupMain is not null || ExtraStudyGroupSecondary is not null;

    public Student WrappedStudent { get; }

    public void AddToGroup(AcademicMobilityGroup group)
    {
        if (ExtraStudyGroupMain is null)
        {
            ExtraStudyGroupMain = group;
        }
        else if (ExtraStudyGroupSecondary is null)
        {
            ExtraStudyGroupSecondary = group;
        }
        else
        {
            throw AcademicMobilityException.AlreadyFullyEnrolled(this);
        }
    }

    public void RemoveFromGroup(AcademicMobilityGroup group)
    {
        if (ExtraStudyGroupMain == group)
        {
            ExtraStudyGroupMain = null;
        }
        else if (ExtraStudyGroupSecondary == group)
        {
            ExtraStudyGroupSecondary = null;
        }
        else
        {
            throw AcademicMobilityException.NotEnrolled(this);
        }
    }
}