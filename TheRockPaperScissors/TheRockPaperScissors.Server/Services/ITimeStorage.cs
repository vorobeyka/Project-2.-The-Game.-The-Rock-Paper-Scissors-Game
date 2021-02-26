using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TheRockPaperScissors.Server.Services
{
    public interface ITimeStorage
    {
        Task<ITimeService> GetById(Guid id);
        Task Add(Guid id, ITimeService timeService);
    }
}
