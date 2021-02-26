using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TheRockPaperScissors.Server.Enums;

namespace TheRockPaperScissors.Server.Models
{
    public class Statistics
    {
        public int Wins { get; set; }
        public int Loses { get; set; }
        public int Draws { get; set; }
        public int Rock { get; set; }
        public int Paper { get; set; }
        public int Scissors { get; set; }
        public string Time { get; set; }

        public void UpdateMove(Move move)
        {
            switch (move)
            {
                case Move.Rock: Rock++; break;
                case Move.Paper: Paper++; break;
                case Move.Scissors: Scissors++; break;
            }
        }

        public void UpdateResult(GameResult result)
        {
            switch (result)
            {
                case GameResult.Win: Wins++; break;
                case GameResult.Draw: Draws++; break;
                case GameResult.Loss: Loses++; break;
            }
        }

        public void UpdateTime(TimeSpan time)
        {
            Time = string.IsNullOrEmpty(Time)
                ? time.ToString()
                : (TimeSpan.Parse(Time) + time).ToString();
        }
    }
}
