using System;
using TheRockPaperScissors.Client.Game;
using System.Collections.Generic;
using System.Text;

namespace TheRockPaperScissors.Client.Menu
{
    public class GameMenu
    {
        public ConsoleColor Color { get; set; }

        public GameMenu(ConsoleColor color)
        {
            Color = color;
        }

        MenuDesign MenuDesign = new MenuDesign();
        MenuValidation MenuValidation = new MenuValidation();

        public void Load(ConsoleColor color)
        {
            Console.ForegroundColor = color;
            MenuDesign.WriteInColor(" Select mode : ", ConsoleColor.Cyan);
            int number = 1;
            foreach (GameType type in Enum.GetValues(typeof(GameType)))
            {
                Console.WriteLine($" {number} - {Enum.GetNames(typeof(GameType))[number-1]}");
                number++;
            }
            int command = MenuValidation.CheckInteger(" Enter command : ", number);
        }
    }
}
