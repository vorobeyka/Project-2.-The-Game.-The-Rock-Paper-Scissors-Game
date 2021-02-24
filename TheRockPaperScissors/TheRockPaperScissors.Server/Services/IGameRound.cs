using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Concurrent;

namespace TheRockPaperScissors.Server.Services
{
    public interface IGameRound
    {
        bool IsOpen { get; }
        ConcurrentDictionary<Guid, string> Moves { get; }
        string GetResult();
    }
}
