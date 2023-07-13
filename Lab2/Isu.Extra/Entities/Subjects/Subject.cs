namespace Isu.Extra.Entities.Subjects;

public record struct Subject
{
    public string CourseName { get; init; }

    public Subject(string courseName)
    {
        CourseName = courseName;
    }
}