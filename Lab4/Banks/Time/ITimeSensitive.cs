namespace Banks.Time;

public interface ITimeSensitive
{
    void SkipTimePeriod(TimeSpan timeSpan);
}