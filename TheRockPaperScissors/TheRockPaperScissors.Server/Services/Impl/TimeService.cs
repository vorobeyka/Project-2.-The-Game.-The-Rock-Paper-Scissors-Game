using System;

namespace TheRockPaperScissors.Server.Services.Impl
{
    public class TimeService : ITimeService
    {
        private DateTime _timeStart;
        private TimeSpan _time = TimeSpan.Zero;
        private TimeSpan _maxTime;

        public void StartTime(TimeSpan maxTime)
        {
            _timeStart = DateTime.UtcNow;
            _maxTime = maxTime;
        }

        public bool IsOutTime()
        {
            _time += DateTime.UtcNow.Subtract(_timeStart);
            _timeStart = DateTime.UtcNow;
            return _time >= _maxTime;
        }

        public TimeSpan GetTime() => _time;
    }
}
