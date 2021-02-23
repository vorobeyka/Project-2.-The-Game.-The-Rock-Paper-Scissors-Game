using System;
using TheRockPaperScissors.Client.Menu;

namespace TheRockPaperScissors.Client
{
    class Program
    {
        static void Main(string[] args)
        {
            MenuView menu = new MenuView();
            menu.Start();
        }
    }
}
