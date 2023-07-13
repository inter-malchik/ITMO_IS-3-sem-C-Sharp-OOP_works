using Isu.Exceptions;

namespace Isu.Entities;

public class Student
{
    public Student(Group group, string name, int id)
    {
        group.AppendStudent(this);
        Group = group;
        Id = id;
        Name = name;
    }

    public int Id { get; }

    public string Name { get; }

    public Group Group { get; private set; }

    public void MoveToGroup(Group newGroup)
    {
        newGroup.AppendStudent(this);
        Group.RemoveStudent(this);
        Group = newGroup;
    }

    public override string ToString()
    {
        return $"({Id}) {Name}";
    }
}