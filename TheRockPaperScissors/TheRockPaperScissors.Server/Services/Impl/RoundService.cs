using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TheRockPaperScissors.Server.Enums;

namespace TheRockPaperScissors.Server.Services.Impl
{
    public class RoundService : IRoundService
    {
        public ConcurrentDictionary<Guid, Move> Moves { get; }
        public bool IsOpen => Moves.Count < 2;

        public RoundService()
        {
            Moves = new ConcurrentDictionary<Guid, Move>();
        }

        public bool AddMove(Guid id, Move move)
        {
            return Moves.TryAdd(id, move);
        }

        public async Task<string> GetResultAsync(Guid id)
        {
            await Task.Delay(100);
            var timer = 0;

            while (Moves.Count == 1 && timer < 20)
            {
                await Task.Delay(TimeSpan.FromSeconds(1));
                timer++;
            }
            if (timer == 20) return "";

            //TODO: return normal result

            return $"{Moves}\n your move - {Moves[id]}";
        }
    }
}
