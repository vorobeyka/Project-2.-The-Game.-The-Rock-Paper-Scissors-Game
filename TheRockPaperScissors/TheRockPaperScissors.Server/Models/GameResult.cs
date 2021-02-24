using System;
using TheRockPaperScissors.Server.Enums;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TheRockPaperScissors.Server.Models
{
    public class GameResult
    {
        public Result Result { get; }

        public DateTime Time { get; }
    }
}
