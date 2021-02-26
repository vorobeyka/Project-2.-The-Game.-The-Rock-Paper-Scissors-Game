using System;
using TheRockPaperScissors.Client.Models;
using TheRockPaperScissors.Client.StatisticsAndRating;
using System.Threading.Tasks;

namespace TheRockPaperScissors.Client.Menu
{
    public class MainMenu
    {
        private readonly MenuDesign _menuDesign = new MenuDesign();
        private readonly GameMenu _gameMenu = new GameMenu();
        private readonly MenuValidation _menuValidation = new MenuValidation();
        private readonly AuthorizationMenu _authorizationMenu = new AuthorizationMenu();
        private readonly Statistics _statistics = new Statistics();

        private User User { get; set; }

        public async Task Load(ConsoleColor color)
        {
            Console.ForegroundColor = color;
            _menuDesign.WriteInColor("\n THE ROCK PAPER SCISSORS by Andrey Basystyi and Emilia Voronova\n", ConsoleColor.Cyan);
            User = await _authorizationMenu.Load(ConsoleColor.Yellow);
            int command = 0;

            while (command != 5 && User != null)
            {
                Console.Clear();
                Console.WriteLine($" USER : {User.Login}");
                _menuDesign.WriteHeader("main menu");
                Console.WriteLine(" 1 - Play\n 2 - Set color\n 3 - Statistics\n 4 - Rating\n 5 - Exit");
                command = _menuValidation.CheckInteger(" Enter number >> ", 5);

                switch (command)
                {
                    case 1:
                        await _gameMenu.Load(MenuDesign.Color, User);
                        break;
                    case 2:
                        _menuDesign.SetConsoleColor();
                        break;
                    case 3:
                        await _statistics.LoadStatistics(User.Login);
                        break;
                    case 4:
                        await _statistics.LoadRating();
                        break;
                    case 5:
                        return;
                }
            }
        }
    }
}
