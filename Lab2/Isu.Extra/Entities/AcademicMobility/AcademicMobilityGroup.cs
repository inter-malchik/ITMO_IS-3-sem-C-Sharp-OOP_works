using Isu.Extra.Entities.Lessons.Schedule;
using Isu.Extra.Entities.People;
using Isu.Extra.Exceptions.AcademicMobility;
using Isu.Extra.Utils;

namespace Isu.Extra.Entities.AcademicMobility;

public class AcademicMobilityGroup
{
    private readonly List<AcademicMobilityStudent> _students;

    public AcademicMobilityGroup(string name, int maxAmount, Schedule extraStudySchedule)
    {
        _students = new List<AcademicMobilityStudent>();
        MaxAmount = maxAmount;
        Name = name;
        Id = GuidGenerator.GenerateId();
        ExtraStudySchedule = extraStudySchedule;
    }

    public Guid Id { get; }

    public IReadOnlyList<AcademicMobilityStudent> Students => _students.AsReadOnly();

    public int MaxAmount { get; }

    public int Amount => _students.Count;

    public int Capacity => MaxAmount - _students.Count;

    public string Name { get; }

    public Schedule ExtraStudySchedule { get; }

    public void AddStudent(AcademicMobilityStudent student)
    {
        if (Capacity == 0)
        {
            throw AcademicMobilityException.NotEnoughPlaces(this);
        }

        student.AddToGroup(this);

        _students.Add(student);
    }

    public void RemoveStudent(AcademicMobilityStudent student)
    {
        if (!_students.Contains(student))
        {
            throw AcademicMobilityException.NotInExtraStudyGroup(this, student);
        }

        student.RemoveFromGroup(this);

        _students.Remove(student);
    }
}