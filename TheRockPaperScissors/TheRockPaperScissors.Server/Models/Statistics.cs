using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TheRockPaperScissors.Server.Models
{
    public class Statistics
    {
        public List<GameResult> Wins { get; }

        public List<GameResult> Draws { get; }

        public List<GameResult> Losses { get; }
    }
}
