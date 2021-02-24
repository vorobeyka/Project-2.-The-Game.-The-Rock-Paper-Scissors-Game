using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TheRockPaperScissors.Server.Services.Impl
{
    public class GameRound : IGameRound
    {
        public ConcurrentDictionary<Guid, string> Moves { get; }
        public bool IsOpen => Moves.Count < 2;

        public GameRound()
        {
            Moves = new ConcurrentDictionary<Guid, string>();
        }

        public string GetResult()
        {
            return null;
        }
    }
}
