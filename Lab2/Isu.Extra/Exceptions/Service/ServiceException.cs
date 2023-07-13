using Isu.Entities;
using Isu.Extra.Entities.Megafaculties;

namespace Isu.Extra.Exceptions.Service;

public class ServiceException : IsuExtraBaseException
{
    private ServiceException(string message)
        : base(message)
    { }

    public static ServiceException
        AlreadyRegistered(Student student)
    {
        return new ServiceException($"student {student.Name} ({student.Id}) is already registered in ExtraService");
    }

    public static ServiceException
        AlreadyRegistered(Megafaculty faculty)
    {
        return new ServiceException($"faculty {faculty.Name} is already registered in ExtraService");
    }

    public static ServiceException
        IsNotRegistered(Megafaculty faculty)
    {
        return new ServiceException($"faculty {faculty.Name} is not registered in ExtraService");
    }
}