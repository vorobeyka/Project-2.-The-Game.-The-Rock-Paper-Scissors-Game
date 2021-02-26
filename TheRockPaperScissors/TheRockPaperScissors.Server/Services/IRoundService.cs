using System;
using System.Threading.Tasks;
using System.Collections.Concurrent;
using TheRockPaperScissors.Server.Enums;
using TheRockPaperScissors.Server.Models;

namespace TheRockPaperScissors.Server.Services
{
    public interface IRoundService
    {
        ConcurrentDictionary<Guid, GameResult> Result { get; set; }
        ITimeService Timer { get; }
        bool IsOpen { get; }

        bool AddMove(Guid id, Move move);
        Task<string> GetResultAsync(Guid id, Statistics statistics, GameType type);
        string GetResult(Guid id);
    }
}
