using System;

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
