using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TheRockPaperScissors.Server.Enums;
using TheRockPaperScissors.Server.Models;
using TheRockPaperScissors.Server.Services.Algorithms;

namespace TheRockPaperScissors.Server.Services.Impl
{
    public class RoundService : IRoundService
    {
        private readonly ConcurrentDictionary<Guid, string> _result = new ConcurrentDictionary<Guid, string>();
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

            var secondId = Moves.First(move => move.Key != id).Key;
            var move1 = Moves[id];
            var move2 = Moves.First(move => move.Key != id).Value;
            _result.TryAdd(id, GetResultString(Moves[id], Moves[secondId]));

            return _result[id];
        }

        public string GetResult(Guid id) => _result[id];

        private string GetResultString(Move firstPlayerMove, Move secondPlayerMove)
        {
            var result = $" You      : {firstPlayerMove}| Opponent : {secondPlayerMove}|~";
            var gameResult = GameAlgorithm.GetRound(firstPlayerMove, secondPlayerMove);
            switch (gameResult)
            {
                case GameResult.Win: return result += "YOU WON";
                case GameResult.Loss: return result += "YOU LOSE";
                default: return result += "DRAW";
            }
        }
    }
}
