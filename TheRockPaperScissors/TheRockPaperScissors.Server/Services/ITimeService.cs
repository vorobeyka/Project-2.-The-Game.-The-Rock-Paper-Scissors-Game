using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TheRockPaperScissors.Server.Services
{
    public interface ITimeService
    {
        void StartTime(TimeSpan maxTime);
        public bool IsOutTime();
        TimeSpan GetTime();
    }
}
