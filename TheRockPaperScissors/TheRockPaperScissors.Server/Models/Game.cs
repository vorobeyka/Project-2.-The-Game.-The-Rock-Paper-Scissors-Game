using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TheRockPaperScissors.Server.Enums;

namespace TheRockPaperScissors.Server.Models
{
    public class Game
    {
        public string UserId { get; set; }
        public GameType Type {get; set;}
        public string GameId { get; set; }
    }
}
