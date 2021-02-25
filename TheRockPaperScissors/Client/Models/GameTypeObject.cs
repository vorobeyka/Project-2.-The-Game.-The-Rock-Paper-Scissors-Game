using System;
using TheRockPaperScissors.Client.Game.Enums;

namespace TheRockPaperScissors.Client.Models
{
    public class GameTypeObject
    {
        public Guid Id { get; set; }

        public GameType GameType { get; set; }

        public string GameId { get; set; }

        public GameTypeObject(Guid id, GameType gameType, string gameId)
        {
            Id = id;
            GameType = gameType;
            GameId = gameId;
        }
    }
}
