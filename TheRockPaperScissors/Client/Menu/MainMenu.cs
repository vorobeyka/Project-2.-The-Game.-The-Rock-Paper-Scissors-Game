using System;
using TheRockPaperScissors.Client.Models;
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
            int menuCommand = 0;

            while (menuCommand != 3)
            {
                Console.WriteLine($" USER : {User.Login}");
                MenuDesign.WriteHeader("MAIN MENU");
                Console.WriteLine(" 1 - Play\n 2 - Set color\n 3 - Exit");
                menuCommand = MenuValidation.CheckInteger(" Enter number ", 3);

                switch (menuCommand)
                {
                    case 1:
                        GameMenu.Load(MenuDesign.Color, User);
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
