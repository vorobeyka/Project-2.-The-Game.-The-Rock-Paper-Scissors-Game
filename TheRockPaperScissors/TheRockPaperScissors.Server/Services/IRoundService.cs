using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Concurrent;
using TheRockPaperScissors.Server.Enums;

namespace TheRockPaperScissors.Server.Services
{
    public interface IRoundService
    {
        bool IsOpen { get; }

        bool AddMove(Guid id, Move move);
        Task<string> GetResultAsync(Guid id);
        ConcurrentDictionary<Guid, Move> Moves { get; }
    }
}
