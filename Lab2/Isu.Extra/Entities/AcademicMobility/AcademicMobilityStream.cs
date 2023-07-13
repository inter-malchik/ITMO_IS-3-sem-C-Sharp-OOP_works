using Isu.Extra.Entities.Lessons.Schedule;
using Isu.Extra.Entities.Megafaculties;
using Isu.Extra.Entities.People;
using Isu.Extra.Entities.Subjects;
using Isu.Extra.Exceptions.AcademicMobility;
using Isu.Extra.Utils;

namespace Isu.Extra.Entities.AcademicMobility;

public class AcademicMobilityStream
{
    private readonly Megafaculty _faculty;
    private readonly List<AcademicMobilityGroup> _groups;

    public AcademicMobilityStream(Subject subject, Megafaculty faculty)
    {
        Subject = subject;
        _groups = new List<AcademicMobilityGroup>();
        Id = GuidGenerator.GenerateId();
        _faculty = faculty;
        _faculty.AppendStudyStream(this);
    }

    public Guid Id { get; }

    public Subject Subject { get; }

    public int MaxAmountTotal => _groups.Select(group => group.MaxAmount).Sum();

    public int AmountTotal => _groups.Select(group => group.Amount).Sum();

    public int CapacityTotal => _groups.Select(group => group.Capacity).Sum();

    public IReadOnlyList<AcademicMobilityGroup> AcademicMobilityGroups => _groups.AsReadOnly();

    public AcademicMobilityGroup CreateGroup(string name, int maxAmount, Schedule extraStudySchedule)
    {
        var newValue = new AcademicMobilityGroup(name, maxAmount, extraStudySchedule);
        _groups.Add(newValue);
        return newValue;
    }

    public AcademicMobilityGroup RegisterStudent(AcademicMobilityStudent student)
    {
        if (MegafacultyParser.Parse(student.WrappedStudent.Group.GroupName).Name == _faculty.Name)
        {
            throw AcademicMobilityException.OwnFacultyExtraStudy(student);
        }

        if (CapacityTotal == 0)
        {
            throw AcademicMobilityException.NotEnoughPlaces(this);
        }

        AcademicMobilityGroup group = _groups.First(group => group.Capacity != 0 || !group.ExtraStudySchedule.Intersects(student.MainSchedule));

        group.AddStudent(student);

        return group;
    }
}