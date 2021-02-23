using System;
using System.Threading.Tasks;
using TheRockPaperScissors.Client.Menu;

namespace TheRockPaperScissors.Client
{
    class Program
    {
        static async Task Main(string[] args)
        {
            MainMenu menu = new MainMenu();
            await menu.Load(ConsoleColor.Yellow);
        }
    }
}
