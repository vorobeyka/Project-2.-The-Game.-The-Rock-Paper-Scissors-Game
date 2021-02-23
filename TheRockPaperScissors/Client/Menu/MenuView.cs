using System;
using TheRockPaperScissors.Client.Models;
using System.Collections.Generic;
using System.Text;

namespace TheRockPaperScissors.Client.Menu
{
    public class MenuView
    {
        private MenuDesign MenuDesign = new MenuDesign();
        private GameMenu gameMenu = new GameMenu();
        private MenuValidation MenuValidation = new MenuValidation();

        public async void Start()
        {
            ClientController clientController = new ClientController();
            MenuDesign.WriteInColor(" THE ROCK PAPER SCISSORS", ConsoleColor.Cyan);
            Console.WriteLine(" 1 - Register\n 2 - Login");
            int authCommand = MenuValidation.CheckInteger(" Enter command : ", 2);
            string login = MenuValidation.InputString(" Enter login : ");
            string password = MenuValidation.InputString(" Enter password : ");
            string token;
            switch (authCommand)
            {
                case 1:
                    throw new NotImplementedException();
                    //await clientController.Registration(login, password);
                    //break;
                case 2:
                    if (login == "q" && password == "qqqqqq")
                        MenuDesign.WriteInColor(" Login successful", ConsoleColor.Cyan);
                    //await clientController.Login(login, password);
                    break;
            }
            Console.WriteLine(" 1 - Play\n 2 - Set color");
            int menuCommand = MenuValidation.CheckInteger(" Enter command : ", 2);
            switch (menuCommand)
            {
                case 1:
                    gameMenu.Load();
                    throw new NotImplementedException();
                case 2:
                    SetConsoleColor();
                    break;
            }
        }

        public void SetConsoleColor()
        {
            Console.WriteLine(" Set console color : ");
            int number = 1;
            foreach (ConsoleColor color in Enum.GetValues(typeof(ConsoleColor)))
            {
                if (color == ConsoleColor.Black)
                    continue;
                MenuDesign.WriteInColor($" {number} - {Enum.GetNames(typeof(ConsoleColor))[number]}", color);
                number++;
            }
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.WriteLine(" Enter number of color: ");
            int.TryParse(Console.ReadLine(), out var num);
            MenuDesign.Color = (ConsoleColor)Enum.GetValues(typeof(ConsoleColor)).GetValue(num);
            Console.ForegroundColor = MenuDesign.Color;
        }
    }
}
