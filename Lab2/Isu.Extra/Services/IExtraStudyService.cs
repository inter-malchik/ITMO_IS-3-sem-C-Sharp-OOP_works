using Isu.Entities;
using Isu.Extra.Entities.AcademicMobility;
using Isu.Extra.Entities.Lessons.Schedule;
using Isu.Extra.Entities.Megafaculties;
using Isu.Extra.Entities.People;
using Isu.Extra.Entities.Subjects;

namespace Isu.Extra.Services;

public interface IExtraStudyService
{
    Megafaculty CreateMegafaculty(string name);

    Subject RegisterExtraStudyCourse(Megafaculty faculty, string name);

    AcademicMobilityStream CreateAcademicMobilityStream(Megafaculty faculty, Subject subject);

    AcademicMobilityGroup CreateAcademicMobilityGroup(AcademicMobilityStream stream, string name, Schedule schedule);

    IReadOnlyCollection<AcademicMobilityStudent> GetStudentsWithoutExtraStudies(Group group);
}