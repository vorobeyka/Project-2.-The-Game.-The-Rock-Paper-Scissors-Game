using System;
using System.Collections.Generic;
using System.Text;

namespace TheRockPaperScissors.Client.Menu
{
    public class MenuView
    {
        public ConsoleColor Color { get; set; }

        public MenuView()
        {
            Color = ConsoleColor.Gray;
        }

        public void Start()
        {
            WriteInColor(" THE ROCK PAPER SCISSORS", ConsoleColor.Cyan);
            Console.WriteLine(" 1 - Register\n 2 - Login");
            int authCommand = CheckInteger(" Enter command : ", 2);
            string login = InputString(" Enter login : ");
            string password = InputString(" Enter password : ");
            string token;
            switch (authCommand)
            {
                case 1:
                    throw new NotImplementedException();
                    Console.WriteLine(" Register successful");
                    break;
                case 2:
                    if (login == "q" && password == "q")
                    {
                        Console.WriteLine(" Login successful");
                        break;
                    }
                    throw new NotImplementedException();

            }
            Console.WriteLine(" 1 - Play\n 2 - Set color");
            int menuCommand = CheckInteger(" Enter command : ", 2);
            switch (menuCommand)
            {
                case 1:
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
                WriteInColor($" {number} - {Enum.GetNames(typeof(ConsoleColor))[number]}", color);
                number++;
            }
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.WriteLine(" Enter number of color: ");
            int.TryParse(Console.ReadLine(), out var num);
            Color = (ConsoleColor)Enum.GetValues(typeof(ConsoleColor)).GetValue(num);
            Console.ForegroundColor = Color;
        }

        public void WriteInColor(string text, ConsoleColor consoleColor)
        {
            Console.ForegroundColor = consoleColor;
            Console.WriteLine(text);
            Console.ForegroundColor = Color;
        }

        public string InputString(string message)
        {
            Console.WriteLine(message);
            string input = Console.ReadLine();
            while (input.Trim().Length < 1)
            {
                Console.WriteLine("Invalid input. " + message);
                input = Console.ReadLine();
            }
            return input;
        }

        public int CheckInteger(string message, int limit)
        {
            Console.WriteLine(message);
            int command;
            while (!int.TryParse(Console.ReadLine().Trim(), out command)
                || command < 1 || command > limit)
                Console.WriteLine("Invalid input. " + message);
            return command;
        }
    }
}
