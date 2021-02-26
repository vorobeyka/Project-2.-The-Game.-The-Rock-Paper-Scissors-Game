using System;
using System.Threading.Tasks;
using TheRockPaperScissors.Client.Game.Enums;

namespace TheRockPaperScissors.Client.Services
{
    public interface IGameService
    {
        public Task<string> StartGame(Guid token);

        public Task<string> StartRound(Guid token, Move move);

        public Task<(bool, string)> GetRoundResult(Guid token);

        public Task<string> GetSeriesResult(Guid token);
    }
}
