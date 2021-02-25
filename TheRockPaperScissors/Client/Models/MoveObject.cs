using TheRockPaperScissors.Client.Game.Enums;

namespace TheRockPaperScissors.Client.Models
{
    public class MoveObject
    {
        public string Id { get; set; } 

        public Move Move { get; set; }

        public MoveObject(string id, Move move)
        {
            Id = id;
            Move = move;
        }
    }
}
