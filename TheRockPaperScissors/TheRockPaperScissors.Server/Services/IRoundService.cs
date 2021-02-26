using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Concurrent;
using TheRockPaperScissors.Server.Enums;
using TheRockPaperScissors.Server.Models;

namespace TheRockPaperScissors.Server.Services
{
    public interface IRoundService
    {
        ITimeService Timer { get; }
        ConcurrentDictionary<Guid, Move> Moves { get; }
        bool IsOpen { get; }

        bool AddMove(Guid id, Move move);
        Task<string> GetResultAsync(Guid id, Statistics statistics);
        string GetResult(Guid id);
    }
}
