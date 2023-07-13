using Isu.Extra.Entities.Lessons;
using Isu.Extra.Entities.Lessons.Schedule;

namespace Isu.Extra.Exceptions.Models;

public class ScheduleException : IsuExtraBaseException
{
    private ScheduleException(string message)
        : base(message)
    { }

    public static ScheduleException
        IncorrectTimePeriod(TimeOnly start, TimeOnly finish)
    {
        return new ScheduleException($"incorrect time period for class: {start} - {finish}");
    }

    public static ScheduleException
        IntersectionOccured(Lesson newLesson)
    {
        return new ScheduleException($"intersection occured when added new lesson {newLesson.Subject}");
    }
}