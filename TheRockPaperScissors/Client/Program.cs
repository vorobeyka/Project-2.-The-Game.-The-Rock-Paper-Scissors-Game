using System;
using TheRockPaperScissors.Client.Menu;
using System.Threading.Tasks;

namespace TheRockPaperScissors.Client
{
    class Program
    {
        static async Task Main(string[] args)
        {
            MenuView menu = new MenuView();
            await menu.Start();
        }
    }
}
