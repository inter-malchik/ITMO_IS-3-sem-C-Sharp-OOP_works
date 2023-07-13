using Isu.Extra.Exceptions.Models;

namespace Isu.Extra.Entities.Lessons;

public struct LessonTime
{
    public LessonTime(TimeOnly start, TimeOnly finish)
    {
        ValidateTimePeriod(start, finish);
        Start = start;
        Finish = finish;
    }

    public TimeOnly Start { get; init; }

    public TimeOnly Finish { get; init; }

    public TimeSpan Timespan => Finish - Start;

    private static void ValidateTimePeriod(TimeOnly startTime, TimeOnly finishTime)
    {
        if (startTime >= finishTime)
        {
            throw ScheduleException.IncorrectTimePeriod(startTime, finishTime);
        }
    }
}