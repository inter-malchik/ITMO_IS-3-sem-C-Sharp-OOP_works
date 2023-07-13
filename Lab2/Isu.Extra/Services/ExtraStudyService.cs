using Isu.Entities;
using Isu.Extra.Entities.AcademicMobility;
using Isu.Extra.Entities.Lessons.Schedule;
using Isu.Extra.Entities.Megafaculties;
using Isu.Extra.Entities.People;
using Isu.Extra.Entities.Subjects;
using Isu.Extra.Exceptions.Models;
using Isu.Extra.Exceptions.Service;

namespace Isu.Extra.Services;

public class ExtraStudyService : IExtraStudyService
{
    private readonly Dictionary<Student, AcademicMobilityStudent> _studentUsers;
    private readonly List<Megafaculty> _faculties;
    private readonly int _maxStudentsInGroup;

    public ExtraStudyService(int maxStudentsInGroup)
    {
        _studentUsers = new Dictionary<Student, AcademicMobilityStudent>();
        _faculties = new List<Megafaculty>();
        _maxStudentsInGroup = maxStudentsInGroup;
    }

    public AcademicMobilityStudent RegisterStudent(Student student, Schedule studySchedule)
    {
        if (_studentUsers.ContainsKey(student))
        {
            throw ServiceException.AlreadyRegistered(student);
        }

        var registeredStudent = new AcademicMobilityStudent(student, studySchedule);

        _studentUsers.Add(student, registeredStudent);

        return registeredStudent;
    }

    public Megafaculty CreateMegafaculty(string name)
    {
        var newFaculty = new Megafaculty(name);

        if (_faculties.Any(faculty => faculty.Name == newFaculty.Name))
        {
            throw ServiceException.AlreadyRegistered(newFaculty);
        }

        _faculties.Add(newFaculty);

        return newFaculty;
    }

    public Subject RegisterExtraStudyCourse(Megafaculty faculty, string name)
    {
        if (!_faculties.Contains(faculty))
        {
            throw ServiceException.IsNotRegistered(faculty);
        }

        var newSubject = new Subject(name);

        if (faculty.HasCourse(newSubject))
        {
            throw MegafacultyException.AlreadyHasCourse(faculty, newSubject);
        }

        faculty.AppendExtraStudyCourse(newSubject);

        return newSubject;
    }

    public AcademicMobilityStream CreateAcademicMobilityStream(Megafaculty faculty, Subject subject)
    {
        if (!_faculties.Contains(faculty))
        {
            throw ServiceException.IsNotRegistered(faculty);
        }

        if (!faculty.HasCourse(subject))
        {
            throw MegafacultyException.DontHaveCourse(faculty, subject);
        }

        var newStream = new AcademicMobilityStream(subject, faculty);

        return newStream;
    }

    public AcademicMobilityGroup CreateAcademicMobilityGroup(AcademicMobilityStream stream, string name, Schedule schedule)
    {
        return stream.CreateGroup(name, _maxStudentsInGroup, schedule);
    }

    public IReadOnlyCollection<Student> GetUnregisteredStudents(Group group)
    {
        return group.Students
            .Where(student => !_studentUsers.ContainsKey(student))
            .ToList()
            .AsReadOnly();
    }

    public IReadOnlyCollection<AcademicMobilityStudent> GetStudentsWithoutExtraStudies(Group group)
    {
        return group.Students
            .Select(student => _studentUsers[student])
            .Where(student => !student.HasExtraStudies)
            .ToList()
            .AsReadOnly();
    }
}