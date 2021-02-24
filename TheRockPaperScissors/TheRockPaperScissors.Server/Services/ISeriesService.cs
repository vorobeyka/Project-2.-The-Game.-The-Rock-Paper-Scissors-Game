using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TheRockPaperScissors.Server.Enums;

namespace TheRockPaperScissors.Server.Services
{
    public interface ISeriesService
    {
        Guid FirstId { get; set; }
        Guid? SecondId { get; set; }
        GameType Type { get; set; }
        string GameId { get; set; }
    }
}
