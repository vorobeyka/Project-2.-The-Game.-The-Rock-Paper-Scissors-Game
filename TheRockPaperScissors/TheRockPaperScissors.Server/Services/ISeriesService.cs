using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TheRockPaperScissors.Server.Enums;
using TheRockPaperScissors.Server.Models;

namespace TheRockPaperScissors.Server.Services
{
    public interface ISeriesService
    {
        Guid FirstId { get; set; }
        Guid? SecondId { get; set; }
        GameType Type { get; set; }
        int RoundCount { get; }
        string GameId { get; set; }

        bool IsRegisteredId(Guid id);
        void SetProperties(Game game);
        Task<IRoundService> GetOpenRoundAsync();
        Task<IRoundService> AddRoundAsync(IRoundService round);
        Task<IRoundService> GetLastRoundAsync();
    }
}
