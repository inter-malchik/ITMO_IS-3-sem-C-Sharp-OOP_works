namespace Isu.Models;

public class StudentIdGenerator
{
    private int _lastId;
    public StudentIdGenerator()
    {
        _lastId = 0;
    }

    public int GetNewId()
    {
        return ++_lastId;
    }
}