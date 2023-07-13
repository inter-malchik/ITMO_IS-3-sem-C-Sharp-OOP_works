using Isu.Extra.Entities.AcademicMobility;
using Isu.Extra.Entities.Subjects;
using Isu.Extra.Utils;

namespace Isu.Extra.Entities.Megafaculties;

public record class Megafaculty
{
    public string Name { get; }

    public Guid Id { get; }

    public Megafaculty(string megafacultyName)
    {
        Name = megafacultyName;
        Id = GuidGenerator.GenerateId();
        _extraStudySubjects = new Dictionary<Subject, List<AcademicMobilityStream>>();
    }

    public bool HasCourse(Subject subject)
    {
        return _extraStudySubjects.Keys.Contains(subject);
    }

    public IReadOnlyList<Subject> GetExtraStudyCourses()
    {
        return _extraStudySubjects.Keys.ToList().AsReadOnly();
    }

    public IReadOnlyList<AcademicMobilityStream> GetStreamsForExtraStudyCourse(Subject subject)
    {
        return _extraStudySubjects[subject].AsReadOnly();
    }

    public IReadOnlyList<AcademicMobilityStream> GetAllExtraStudyStreams()
    {
        var answer = new List<AcademicMobilityStream>();

        foreach (List<AcademicMobilityStream> streamsForSubject in _extraStudySubjects.Values)
        {
            answer.AddRange(streamsForSubject);
        }

        return answer.AsReadOnly();
    }

    public void AppendExtraStudyCourse(Subject subject)
    {
        _extraStudySubjects.Add(subject, new List<AcademicMobilityStream>());
    }

    public void AppendStudyStream(AcademicMobilityStream stream)
    {
        _extraStudySubjects[stream.Subject].Add(stream);
    }

    private Dictionary<Subject, List<AcademicMobilityStream>> _extraStudySubjects;
}