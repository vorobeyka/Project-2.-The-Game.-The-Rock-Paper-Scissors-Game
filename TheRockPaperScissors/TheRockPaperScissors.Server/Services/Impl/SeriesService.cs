using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TheRockPaperScissors.Server.Enums;

namespace TheRockPaperScissors.Server.Services.Impl
{
    public class SeriesService : ISeriesService
    {
        public Guid FirstId { get; set; }
        public Guid? SecondId { get; set; }
        public GameType Type { get; set; }
        public string GameId { get; set; }
    }
}
