using Isu.Exceptions;
using Isu.Extra.Exceptions.Models;
using Isu.Extra.Utils;

namespace Isu.Extra.Entities.Lessons.Schedule;

public record struct DaySchedule
{
    private IReadOnlyCollection<Lesson> _lessons;
    private DaySchedule(List<Lesson> lessons)
    {
        _lessons = lessons;
    }

    public bool Intersects(DaySchedule other)
    {
        foreach (Lesson otherLesson in other._lessons)
        {
            if (_lessons.Any(lesson => lesson.Intersects(otherLesson)))
            {
                return true;
            }
        }

        return false;
    }

    public class DayScheduleBuilder
    {
        private readonly List<Lesson> _lessons;

        public DayScheduleBuilder()
        {
            _lessons = new List<Lesson>();
        }

        public DayScheduleBuilder AddLesson(Lesson lesson)
        {
            ValidateIntersection(_lessons, lesson);
            _lessons.Add(lesson);
            return this;
        }

        public DaySchedule Build()
        {
            return new DaySchedule(_lessons);
        }

        private static void ValidateIntersection(List<Lesson> lessons, Lesson newLesson)
        {
            if (lessons.Any(lesson => lesson.Intersects(newLesson)))
            {
                throw ScheduleException.IntersectionOccured(newLesson);
            }
        }
    }
}