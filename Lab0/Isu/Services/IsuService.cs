using System.Runtime.CompilerServices;
using Isu.Entities;
using Isu.Exceptions;
using Isu.Models;

namespace Isu.Services;

public class IsuService : IIsuService
{
    private readonly int _maxStudentsInGroup;
    private readonly StudentIdGenerator _idGenerator;
    private readonly List<Group> _allGroups;

    public IsuService(int maxStudents = 25)
    {
        _idGenerator = new StudentIdGenerator();
        _allGroups = new List<Group>();
        _maxStudentsInGroup = maxStudents;
    }

    public Group AddGroup(GroupName name)
    {
        if (FindGroup(name) is not null)
            throw new GroupWithThisNameAlreadyExistsException(name);

        var newGroup = new Group(name, _maxStudentsInGroup);
        _allGroups.Add(newGroup);
        return newGroup;
    }

    public Student AddStudent(Group group, string name)
    {
        Group foundGroup = FindGroup(group.GroupName) ?? throw new GroupIsNotTrackedException(group);

        return new Student(foundGroup, name, _idGenerator.GetNewId());
    }

    public Student GetStudent(int id)
    {
        return FindStudent(id) ?? throw new StudentNotFoundException(id);
    }

    public Student? FindStudent(int id)
    {
        return _allGroups
            .SelectMany(group => group.Students)
            .FirstOrDefault(student => student.Id == id);
    }

    public List<Student> FindStudents(GroupName groupName)
    {
        Group? possibleGroup = FindGroup(groupName);

        if (possibleGroup is not null)
            return possibleGroup.Students.ToList();

        return new List<Student>();
    }

    public List<Student> FindStudents(CourseNumber courseNumber)
    {
        return FindGroups(courseNumber)
            .SelectMany(group => group.Students)
            .ToList();
    }

    public Group? FindGroup(GroupName groupName)
    {
        return _allGroups
            .FirstOrDefault(group => group.GroupName.FullName == groupName.FullName);
    }

    public List<Group> FindGroups(CourseNumber courseNumber)
    {
        return _allGroups
            .Where(group => group.GroupName.CourseNumber.Number == courseNumber.Number)
            .ToList();
    }

    public void ChangeStudentGroup(Student student, Group newGroup)
    {
        student.MoveToGroup(newGroup);
    }
}