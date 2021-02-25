using System;
using System.Collections.Generic;
using TheRockPaperScissors.Client.Models;
using System.Text;

namespace TheRockPaperScissors.Client.Game
{
    public class Game
    {
        public Guid Player { get; set; }

        public GameResult GameResult { get; set; }

        public Guid GameCode { get; set; }

        public Game(Guid player)
        {
            Player = player;
        }
    }
}
