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
        public ConcurrentDictionary<Guid, GameResult> Result { get; set; }
        public ITimeService Timer { get; }
        public ConcurrentDictionary<Guid, Move> Moves { get; }
        public bool IsOpen => Moves.Count < 2;

        public RoundService(ITimeService timeService)
        {
            Result = new ConcurrentDictionary<Guid, GameResult>();
            Moves = new ConcurrentDictionary<Guid, Move>();
            Timer = timeService;
            Timer.StartTime(TimeSpan.FromSeconds(20));
        }

        public bool AddMove(Guid id, Move move)
        {
            if (Timer.IsOutTime()) return true;
            return Moves.TryAdd(id, move);
        }

        public async Task<string> GetResultAsync(Guid id, Statistics statistics)
        {
            await Task.Delay(100);
            var timer = 0;

            if (Timer.IsOutTime()) return "";
            while (Moves.Count == 1 && timer < 38)
            {
                await Task.Delay(500);
                timer++;
            }
            if (timer == 38) return "";

            var secondId = Moves.First(move => move.Key != id).Key;
            var move1 = Moves[id];
            var move2 = Moves.First(move => move.Key != id).Value;

            _result.TryAdd(id, GetResultString(Moves[id], Moves[secondId], statistics));

            return _result[id];
        }

        public string GetResult(Guid id) => Moves.Count != 1 ? _result[id] : null;

        private string GetResultString(Move firstPlayerMove, Move secondPlayerMove, Statistics statistics)
        {
            var result = $" You      : {firstPlayerMove}| Opponent : {secondPlayerMove}|~";
            var gameResult = GameAlgorithm.GetRound(firstPlayerMove, secondPlayerMove);
            statistics.UpdateMove(firstPlayerMove);
            statistics.UpdateResult(gameResult);

            switch (gameResult)
            {
                case GameResult.Win: return result += "YOU WON";
                case GameResult.Loss: return result += "YOU LOSE";
                default: return result += "DRAW";
            }
        }
    }
}
