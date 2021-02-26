using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TheRockPaperScissors.Server.Enums;
using TheRockPaperScissors.Server.Exceptions;
using TheRockPaperScissors.Server.Models;

namespace TheRockPaperScissors.Server.Services.Impl
{
    public class SeriesService : ISeriesService
    {
        private readonly IList<IRoundService> _rounds = new List<IRoundService>();
        private readonly SemaphoreSlim _semaphoreSlim = new SemaphoreSlim(1, 1);

        public ITimeService Timer { get; }
        public Guid? FirstId { get; set; }
        public Guid? SecondId { get; set; }
        public GameType Type { get; set; }
        public string GameId { get; set; }
        public int RoundCount => _rounds.Count();

        public SeriesService(ITimeService timeService)
        {
            Timer = timeService;
            Timer.StartTime(TimeSpan.FromSeconds(300));
        }

        public bool IsRegisteredId(Guid id)
        {
            return id == FirstId || id == SecondId;
        }

        public void SetProperties(Game game)
        {
            var id = Guid.Parse(game.UserId);
            Type = game.Type;

            switch (Type)
            {
                case GameType.Training: SetTraining(id); break;
                case GameType.Private: SetPrivate(id, game.GameId); break;
                case GameType.Public: SetPublic(id); break;
            }
        }

        private void SetTraining(Guid id)
        {
            FirstId = id;
            SecondId = Guid.NewGuid();
        }

        private void SetPrivate(Guid id, string gameId)
        {
            if (gameId == null)
            {
                FirstId = id;
                GameId = new Random().Next(1000, 9999).ToString();
            }
            else
            {
                if (gameId != GameId) throw new SeriesException();
                SecondId = id;
            }
        }

        private void SetPublic(Guid id)
        {
            if (FirstId == default) FirstId = id;
            else SecondId = id;
        }

        public async Task<IRoundService> GetOpenRoundAsync()
        {
            await _semaphoreSlim.WaitAsync();
            var result = _rounds.FirstOrDefault(round => round.IsOpen);
            _semaphoreSlim.Release();
            return result;
        }

        public async Task<IRoundService> AddRoundAsync(IRoundService round)
        {
            await _semaphoreSlim.WaitAsync();
            _rounds.Add(round);
            _semaphoreSlim.Release();
            return round;
        }

        public async Task<IRoundService> GetLastRoundAsync()
        {
            await _semaphoreSlim.WaitAsync();
            var result = _rounds.Last();
            _semaphoreSlim.Release();
            return result;
        }

        public string GetResult(Guid id)
        {
            var wins = _rounds.Where(round => round.Result[id] == GameResult.Win).Count();
            var lose = _rounds.Where(round => round.Result[id] == GameResult.Loss).Count();
            var stats = $"wins {wins} : {lose} loses";
            var roundResults = _rounds.Where(round => !round.IsOpen)
                                      .Select(round => round.GetResult(id) + "|").ToArray();

            for (int i = 0; i < roundResults.Length; i++)
            {
                roundResults[i] = $" Round {i + 1}|" + roundResults[i] + " " + new string('_', 16) + "|";
            }

            return string.Concat(roundResults) + " " + stats;
        }
    }
}
