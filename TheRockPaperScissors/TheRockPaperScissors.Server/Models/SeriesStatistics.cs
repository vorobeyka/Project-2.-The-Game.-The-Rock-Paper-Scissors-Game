using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TheRockPaperScissors.Server.Enums;

namespace TheRockPaperScissors.Server.Models
{
    public class SeriesStatistics
    {
        public IList<GameResult> Results { get; }
        public IList<Move> Moves { get; }
        public DateTime Time { get; set; }

        public SeriesStatistics()
        {
            Results = new List<GameResult>();
            Moves = new List<Move>();
        }
    }
}
