using Isu.Extra.Utils;

namespace Isu.Extra.Entities.People;

public class Teacher
{
    public Teacher(string name)
    {
        Name = name;
        Id = GuidGenerator.GenerateId();
    }

    public string Name { get; }

    public Guid Id { get; }
}