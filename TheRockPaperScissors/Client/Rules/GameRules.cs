using System;
using TheRockPaperScissors.Client.Menu;
using System.Text;

namespace TheRockPaperScissors.Client.Rules
{
    public class GameRules
    {
        private readonly MenuDesign _menuDesign = new MenuDesign();

        public void LoadRules()
        {
            Console.Clear();
            _menuDesign.WriteInColor("\n Hello, player.", ConsoleColor.Cyan);
            _menuDesign.WriteInColor(" Welcome to The Rock Scissors Paper:Cyberversion by Emilia Voronova and Andrey Basystyi\n", ConsoleColor.Cyan);
            _menuDesign.WriteHeader("MODES");
            Console.WriteLine(" You can play in three modes : ");
            Console.WriteLine(" - Test mode. Training game of three rounds with the bot, who makes random moves.");
            Console.WriteLine(" - Private mode. Create a room and share an identifier with your friend.");
            Console.WriteLine("   Your friend should connect to your room using the identifier.");
            Console.WriteLine(" - Public mode. Play with random player.");
            _menuDesign.WriteHeader("THE RULES");
            Console.WriteLine(" You only have three options : Rock, Paper, and Scissors.");
            Console.WriteLine(" Rock beats scissors, Scissors beat paper, Paper beats Rock.");
            Console.WriteLine(" Every game session consists of rounds.\n Each round last 20 seconds and the session lasts 5 minutes.");
            Console.WriteLine(" You can end the game by not making a move.");
            _menuDesign.WriteHeader("STATISTICS");
            Console.WriteLine(" You can appear in Rating after having minimum of 10 results in multiplayer modes.");
            Console.WriteLine(" You also have your personal Statistics.");
            _menuDesign.WriteHeader("ADDITIONAL");
            Console.WriteLine(" You can change the color of your interface in Set Color settings.");
            _menuDesign.WriteInColor("\n You need to login to start playing. And let the force be with you!", ConsoleColor.Cyan);
            Console.Write("\n Press ANY KEY to go back >>");
            Console.ReadKey();
        }
    }
}
