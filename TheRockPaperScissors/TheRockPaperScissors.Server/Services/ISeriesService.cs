using System;
using System.Threading.Tasks;
using TheRockPaperScissors.Server.Enums;
using TheRockPaperScissors.Server.Models;

namespace TheRockPaperScissors.Server.Services
{
    public interface ISeriesService
    {
        ITimeService Timer { get; }
        Guid? FirstId { get; set; }
        Guid? SecondId { get; set; }
        GameType Type { get; set; }
        string GameId { get; set; }
        int RoundCount { get; }

        bool IsRegisteredId(Guid id);
        void SetProperties(Game game);
        Task<IRoundService> GetOpenRoundAsync();
        Task<IRoundService> AddRoundAsync(IRoundService round);
        Task<IRoundService> GetLastRoundAsync();
        string GetResult(Guid id);
    }
}
