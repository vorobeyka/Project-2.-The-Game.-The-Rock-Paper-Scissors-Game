using System;

namespace TheRockPaperScissors.Client.Menu
{
    public class MenuDesign
    {
        public static ConsoleColor Color = ConsoleColor.Yellow;
        private readonly MenuValidation _menuValidation = new MenuValidation();

        public void WriteInColor(string text, ConsoleColor consoleColor)
        {
            Console.ForegroundColor = consoleColor;
            Console.Write(text);
            Console.ForegroundColor = Color;
        }

        public void SetConsoleColor()
        {
            WriteHeader("SET MENU COLOR");
            int number = 2;

            foreach (ConsoleColor color in Enum.GetValues(typeof(ConsoleColor)))
            {
                if (color == ConsoleColor.Black || color == ConsoleColor.DarkBlue)
                    continue;

                WriteInColor($" {number-1} - {Enum.GetNames(typeof(ConsoleColor))[number]}\n", color);
                number++;
            }

            Console.ForegroundColor = ConsoleColor.Yellow;
            var num = _menuValidation.CheckInteger(" Choose color >> ", number - 2);
            Color = (ConsoleColor)Enum.GetValues(typeof(ConsoleColor)).GetValue(num + 1);
            Console.ForegroundColor = Color;
            Console.Clear();
        }

        public void WriteHeader(string text)
        {
            ConsoleColor color = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Cyan;
            string decoration = " ";

            for (int i = 0; i < text.Length; i++)
                decoration += "-";

            Console.WriteLine($"\n {text.ToUpper()}\n{decoration}");
            Console.ForegroundColor = color;
        }
    }
}
