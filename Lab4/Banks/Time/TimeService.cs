namespace Banks.Time;

public class TimeService
{
     private readonly List<ITimeSensitive> _subscribers;
     private DateTime _dateTime;

     public TimeService()
     {
          _subscribers = new List<ITimeSensitive>();
          _dateTime = DateTime.Now;
     }

     public DateTime GetTime => _dateTime;

     public void Subscribe(ITimeSensitive sensitiveObject)
     {
          _subscribers.Add(sensitiveObject);
     }

     public void RegisterTimeDifference(TimeSpan timeSpan)
     {
          _dateTime += timeSpan;

          foreach (var subscriber in _subscribers)
          {
               subscriber.SkipTimePeriod(timeSpan);
          }
     }
}