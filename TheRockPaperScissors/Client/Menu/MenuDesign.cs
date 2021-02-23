using System;
using System.Collections.Generic;
using System.Text;

namespace TheRockPaperScissors.Client.Menu
{
    public class MenuDesign
    {
        public ConsoleColor Color { get; set; }

        public MenuDesign()
        {
            Color = ConsoleColor.Gray;
        }

        public void WriteInColor(string text, ConsoleColor consoleColor)
        {
            Console.ForegroundColor = consoleColor;
            Console.WriteLine(text);
            Console.ForegroundColor = Color;
        }
    }
}
