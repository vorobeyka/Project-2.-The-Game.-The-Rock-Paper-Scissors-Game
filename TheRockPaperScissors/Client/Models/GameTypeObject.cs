using TheRockPaperScissors.Client.Game.Enums;

namespace TheRockPaperScissors.Client.Models
{
    public class GameTypeObject
    {
        public string UserId { get; set; }

        public GameType Type { get; set; }

        public string GameId { get; set; }

        public GameTypeObject(string id, GameType gameType, string gameId)
        {
            UserId = id;
            Type = gameType;
            GameId = gameId;
        }
    }
}
