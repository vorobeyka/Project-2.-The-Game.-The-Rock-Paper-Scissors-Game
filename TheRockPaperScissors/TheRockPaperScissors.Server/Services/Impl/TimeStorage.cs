using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TheRockPaperScissors.Server.Services.Impl
{
    public class TimeStorage :ITimeStorage
    {
        private ConcurrentDictionary<Guid, ITimeService> _times;

        public TimeStorage()
        {
            _times = new ConcurrentDictionary<Guid, ITimeService>();
        }

        public async Task Add(Guid id, ITimeService timeService)
        {
            await Task.Delay(1);
            _times.TryAdd(id, timeService);
        }

        public async Task<ITimeService> GetById(Guid id)
        {
            await Task.Delay(1);
            return _times[id];
        }
    }
}
