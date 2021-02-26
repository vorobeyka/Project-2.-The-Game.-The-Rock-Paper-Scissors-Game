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
        private readonly ConcurrentDictionary<Guid, Move> _moves;

        private GameType? _type = null;
        public ConcurrentDictionary<Guid, GameResult> Result { get; set; }
        public ITimeService Timer { get; }
        public bool IsOpen => _moves.Count < 2;

        public RoundService(ITimeService timeService)
        {
            Result = new ConcurrentDictionary<Guid, GameResult>();
            _moves = new ConcurrentDictionary<Guid, Move>();
            Timer = timeService;
            Timer.StartTime(TimeSpan.FromSeconds(20));
        }

        public bool AddMove(Guid id, Move move)
        {
            if (Timer.IsOutTime()) return true;
            return _moves.TryAdd(id, move);
        }

        public async Task<string> GetResultAsync(Guid id, Statistics statistics, GameType type)
        {
            await Task.Delay(100);
            var timer = 0;

            if (Timer.IsOutTime()) return "";
            if (type == GameType.Training) MoveBot();
            while (_moves.Count == 1 && timer < 38)
            {
                await Task.Delay(500);
                timer++;
            }
            if (timer == 38) return "";

            var secondId = _moves.First(move => move.Key != id).Key;
            var move1 = _moves[id];
            var move2 = _moves.First(move => move.Key != id).Value;

            _result.TryAdd(id, GetResultString(_moves[id], _moves[secondId], statistics, id));

            return _result[id];
        }

        private void MoveBot()
        {
            var rnd = new Random().Next(0, 3);
            AddMove(Guid.NewGuid(), (Move)rnd);
            _type = GameType.Training;
        }

        public string GetResult(Guid id) => _moves.Count != 1 ? _result[id] : null;

        private string GetResultString(Move firstPlayerMove, Move secondPlayerMove, Statistics statistics, Guid id)
        {
            var result = $" You      : {firstPlayerMove}| Opponent : {secondPlayerMove}|~";
            var gameResult = GameAlgorithm.GetRound(firstPlayerMove, secondPlayerMove);
            Result[id] = gameResult;
            if (_type != GameType.Training)
            {
                statistics.UpdateMove(firstPlayerMove);
                statistics.UpdateResult(gameResult);
            }

            switch (gameResult)
            {
                case GameResult.Win: return result += "YOU WON";
                case GameResult.Loss: return result += "YOU LOSE";
                default: return result += "DRAW";
            }
        }
    }
}
