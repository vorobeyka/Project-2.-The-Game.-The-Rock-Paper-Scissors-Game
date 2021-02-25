using System;
using TheRockPaperScissors.Client.Models;
using TheRockPaperScissors.Client.Game.Enums;
using System.Threading.Tasks;

namespace TheRockPaperScissors.Client.Menu
{
    public class MainMenu
    {
        private readonly MenuDesign MenuDesign = new MenuDesign();
        private readonly GameMenu GameMenu = new GameMenu();
        private readonly MenuValidation MenuValidation = new MenuValidation();
        private readonly AuthorizationMenu AuthorizationMenu = new AuthorizationMenu();
        private User User { get; set; }

        public async Task Load(ConsoleColor color)
        {
            Console.ForegroundColor = color;
            MenuDesign.WriteInColor("\n THE ROCK PAPER SCISSORS \n", ConsoleColor.Cyan);
            User = await AuthorizationMenu.Load(ConsoleColor.Yellow);
            int command = 0;

            while (command != 3)
            {
                //Console.Clear();
                Console.WriteLine($" USER : {User.Login}");
                MenuDesign.WriteHeader("main menu");
                Console.WriteLine(" 1 - Play\n 2 - Set color\n 3 - Exit");
                command = MenuValidation.CheckInteger(" Enter number >> ", 3);

                switch (command)
                {
                    case 1:
                        await GameMenu.Load(MenuDesign.Color, User);
                        break;
                    case 2:
                        MenuDesign.SetConsoleColor();
                        break;
                    case 3:
                        return;
                }
            }
        }
    }
}
