using Isu.Extra.Entities.AcademicMobility;
using Isu.Extra.Entities.People;
using Isu.Extra.Entities.Subjects;

namespace Isu.Extra.Entities.Lessons;

public record struct Lesson
{
    public Subject Subject { get; init; }
    public LessonTime Time { get; init; }
    public AcademicMobilityGroup? Group { get; init; }
    public Teacher? Teacher { get; init; }
    public Classroom? Classroom { get; init; }

    public bool Intersects(Lesson other)
    {
        if (Time.Start < other.Time.Start && other.Time.Finish < Time.Finish)
        {
            return true;
        }

        if (other.Time.Start < Time.Start && Time.Finish < other.Time.Finish)
        {
            return true;
        }

        if (other.Time.Start - Time.Start < Time.Timespan)
        {
            return true;
        }

        return false;
    }
}