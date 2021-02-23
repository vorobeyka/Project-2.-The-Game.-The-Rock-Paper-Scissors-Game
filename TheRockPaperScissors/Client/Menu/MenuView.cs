using System;
using TheRockPaperScissors.Client.Models;
using System.Threading.Tasks;
using System.Text;

namespace TheRockPaperScissors.Client.Menu
{
    public class MenuView
    {
        private MenuDesign MenuDesign = new MenuDesign();
        private static readonly ConsoleColor color = MenuDesign.Color;
        private GameMenu GameMenu = new GameMenu(color);
        private MenuValidation MenuValidation = new MenuValidation();
        private ClientController ClientController = new ClientController();

        public async Task Start()
        {
            MenuDesign.WriteInColor(" THE ROCK PAPER SCISSORS", ConsoleColor.Cyan);
            Console.WriteLine(" 1 - Register\n 2 - Login");
            int authCommand = MenuValidation.CheckInteger(" Enter command : ", 2);
            string login = MenuValidation.InputString(" Enter login : ");
            string password = MenuValidation.InputString(" Enter password : ");
            string token;
            switch (authCommand)
            {
                case 1:
                    //throw new NotImplementedException();
                    //Console.WriteLine(" fghhhhhhhhhh");
                    token = await ClientController.Registration(login, password);
                    Console.WriteLine(token);
                    break;
                case 2:
                    if (login == "q" && password == "qqqqqq")
                        MenuDesign.WriteInColor(" Login successful", ConsoleColor.Cyan);
                    //token = await ClientController.Login(login, password);
                    //Console.WriteLine(token);
                    break;
            }
            Console.WriteLine(" 1 - Play\n 2 - Set color\n 3 - Exit");
            int menuCommand = 0;
            while (menuCommand != 3)
            {
                menuCommand = MenuValidation.CheckInteger(" Enter command : ", 3);
                switch (menuCommand)
                {
                    case 1:
                        GameMenu.Load(MenuDesign.Color);
                        break;
                        //throw new NotImplementedException();
                    case 2:
                        MenuDesign.SetConsoleColor();
                        Console.WriteLine(" 1 - Play\n 2 - Set color\n 3 - Exit");
                        break;
                }
            }
        }

        
    }
}
