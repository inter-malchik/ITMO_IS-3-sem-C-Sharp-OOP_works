namespace Isu.Extra.Entities.Lessons.Schedule;

public class Schedule
{
    private DaySchedule[] _oddWeek;

    private DaySchedule[] _evenWeek;

    private Schedule(DaySchedule[] oddWeek, DaySchedule[] evenWeek)
    {
        _oddWeek = oddWeek;
        _evenWeek = evenWeek;
    }

    public IReadOnlyCollection<DaySchedule> FullSchedule => _oddWeek.Concat(_evenWeek).ToList();

    public IReadOnlyCollection<DaySchedule> OddWeekSchedule => _oddWeek.ToList();

    public IReadOnlyCollection<DaySchedule> EvenWeekSchedule => _evenWeek.ToList();

    public bool Intersects(Schedule other)
    {
        foreach (DaySchedule otherSchedule in other.FullSchedule)
        {
            if (FullSchedule.Any(lesson => lesson.Intersects(otherSchedule)))
            {
                return true;
            }
        }

        return false;
    }

    public class ScheduleBuilder
    {
        private readonly DaySchedule[] _oddWeek;
        private readonly DaySchedule[] _evenWeek;

        public ScheduleBuilder()
        {
            _oddWeek = new DaySchedule[7];
            _evenWeek = new DaySchedule[7];
        }

        public ScheduleBuilder AddSchedule(Weeks week, WeekDays weekDay, DaySchedule schedule)
        {
            switch (week)
            {
                case Weeks.OddWeek:
                    AddSchedule(_oddWeek, weekDay, schedule);
                    break;

                case Weeks.EvenWeek:
                    AddSchedule(_evenWeek, weekDay, schedule);
                    break;
            }

            return this;
        }

        public Schedule Build()
        {
            return new Schedule(_oddWeek, _evenWeek);
        }

        private void AddSchedule(DaySchedule[] week, WeekDays weekDay, DaySchedule schedule)
        {
            switch (weekDay)
            {
                case WeekDays.Monday:
                    week[0] = schedule;
                    break;

                case WeekDays.Tuesday:
                    week[1] = schedule;
                    break;

                case WeekDays.Wednesday:
                    week[2] = schedule;
                    break;

                case WeekDays.Thursday:
                    week[3] = schedule;
                    break;

                case WeekDays.Friday:
                    week[4] = schedule;
                    break;

                case WeekDays.Saturday:
                    week[5] = schedule;
                    break;

                case WeekDays.Sunday:
                    week[6] = schedule;
                    break;
            }
        }
    }
}