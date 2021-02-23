using System;
using System.Collections.Generic;
using System.Text;

namespace TheRockPaperScissors.Client.Menu
{
    public class MenuDesign
    {
        public static ConsoleColor Color = Console.ForegroundColor;

        public void WriteInColor(string text, ConsoleColor consoleColor)
        {
            Console.ForegroundColor = consoleColor;
            Console.WriteLine(text);
            Console.ForegroundColor = Color;
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

        public ConsoleColor GetCurrentColor()
        {
            return Color;
        }
    }
}
