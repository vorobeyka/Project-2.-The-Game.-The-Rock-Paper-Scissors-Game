using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TheRockPaperScissors.Server.Enums;
using TheRockPaperScissors.Server.Models;

namespace TheRockPaperScissors.Server.Services.Algorithms
{
    public static class GameAlgorithm
    {
        private static readonly int[][] _gameMatrix = new int[3][]
        {
            new int[3] { 0, 1, -1 },
            new int[3] { -1, 0, 1 },
            new int[3] { 1, -1, 0 }
        };

        public static GameResult GetRound(Move move1, Move move2)
        {
            int i = (int)move1;
            int j = (int)move2;
            return (_gameMatrix[i][j]) switch
            {
                1 => GameResult.Loss,
                -1 => GameResult.Win,
                _ => GameResult.Draw,
            };
        }
    }
}
