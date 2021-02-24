using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TheRockPaperScissors.Server.Enums;

namespace TheRockPaperScissors.Server.Services.Impl
{
    public class SeriesService : ISeriesService
    {
        private readonly SemaphoreSlim _semaphoreSlim = new SemaphoreSlim(1, 1);
        public Guid FirstId { get; set; }
        public Guid? SecondId { get; set; }
        public GameType Type { get; set; }
        public string GameId { get; set; }
        public IEnumerable<IGameRound> Rounds { get; set; }

        public bool IsRegisteredId(Guid id)
        {
            return id == FirstId || id == SecondId;
        }

        public async Task<IGameRound> GetOpenRoundAsync()
        {
            await _semaphoreSlim.WaitAsync();
            var result = Rounds.FirstOrDefault(round => round.IsOpen);
            _semaphoreSlim.Release();
            return result;
        }

    }
}
