using System;
using System.Collections.Generic;
using System.Text;

namespace TheRockPaperScissors.Client.Menu
{
    public class MenuDesign
    {
        public static ConsoleColor Color = ConsoleColor.Yellow;

        public void WriteInColor(string text, ConsoleColor consoleColor)
        {
            Console.ForegroundColor = consoleColor;
            Console.WriteLine(text);
            Console.ForegroundColor = Color;
        }

        public void SetConsoleColor()
        {
            WriteHeader("SET MENU COLOR");
            int number = 1;

            foreach (ConsoleColor color in Enum.GetValues(typeof(ConsoleColor)))
            {
                if (color == ConsoleColor.Black)
                    continue;
                WriteInColor($" {number} - {Enum.GetNames(typeof(ConsoleColor))[number]}", color);
                number++;
            }

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(" Choose color");
            int.TryParse(Console.ReadLine(), out var num);
            Color = (ConsoleColor)Enum.GetValues(typeof(ConsoleColor)).GetValue(num);
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
            Console.WriteLine($"\n {text}\n{decoration}");
            Console.ForegroundColor = color;
        }

        public ConsoleColor GetCurrentColor()
        {
            return Color;
        }
    }
}
