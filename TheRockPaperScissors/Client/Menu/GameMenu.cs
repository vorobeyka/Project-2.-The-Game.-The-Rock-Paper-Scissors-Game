using System;
using TheRockPaperScissors.Client.Game.Enums;
using TheRockPaperScissors.Client.Game;
using TheRockPaperScissors.Client.Models;

namespace TheRockPaperScissors.Client.Menu
{
    public class GameMenu
    {
        private readonly MenuDesign MenuDesign = new MenuDesign();
        private readonly MenuValidation MenuValidation = new MenuValidation();
        private readonly GameModes GameModes = new GameModes();

        //public ConsoleColor Color { get; set; }

        /*public GameMenu(ConsoleColor color)
        {
            Color = color;
        }*/

        public void Load(ConsoleColor color, User user)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.ForegroundColor = color;
            Console.Clear();
            MenuDesign.WriteHeader("SELECT MODE");
            int number = 1;
            foreach (GameType type in Enum.GetValues(typeof(GameType)))
            {
                Console.WriteLine($" {number} - {Enum.GetNames(typeof(GameType))[number-1]}");
                number++;
            }
            int command = MenuValidation.CheckInteger(" Enter number ", number);
            switch (command)
            {
                case 1:
                    GameModes.PlayInTrainingMode(user.Id);
                    break;
                    //throw new NotImplementedException();
                case 2:
                    //GameModes.PlayInPrivateMode();
                    //Console.Clear();
                    throw new NotImplementedException();
                case 3:
                    //GameModes.PlayInPublicMode();
                    //Console.Clear();
                    throw new NotImplementedException();
            }
        }
    }
}
