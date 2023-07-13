using Isu.Exceptions;
using Isu.Models;

namespace Isu.Entities;

public class Group
{
    private readonly int _maxAmount;
    private readonly List<Student> _students;

    public Group(GroupName groupName, int maxAmount)
    {
        GroupName = groupName;
        _students = new List<Student>();
        _maxAmount = maxAmount;
    }

    public GroupName GroupName { get; }

    public IReadOnlyCollection<Student> Students => _students.AsReadOnly();

    public void AppendStudent(Student student)
    {
        if (_students.Count == _maxAmount)
            throw new MaxStudentsPerGroupException(this);

        if (_students.Contains(student))
            throw new StudentAlreadyPresentInGroupException(student);

        _students.Add(student);
    }

    public void RemoveStudent(Student student)
    {
        if (!_students.Remove(student))
            throw new StudentIsNotPresentInGroupException(student, this);
    }

    public override string ToString()
    {
        return GroupName.ToString();
    }
}