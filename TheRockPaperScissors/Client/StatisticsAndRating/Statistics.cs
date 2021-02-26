using System;
using System.Threading.Tasks;
using TheRockPaperScissors.Client.Menu;
using TheRockPaperScissors.Client.Services;

namespace TheRockPaperScissors.Client.StatisticsAndRating
{
    public class Statistics
    {
        private readonly StatisticsService _statisticsService = new StatisticsService();
        private readonly MenuDesign _menuDesign = new MenuDesign();

        public async Task LoadStatistics(string login)
        {
            Console.Clear();
            _menuDesign.WriteHeader("statistics");
            string statistics = await _statisticsService.GetStatistics(login);
            string[] stats = statistics.Split(".")[0].Replace("\"", "").Split("|");
            string[] headers = new string[] { "Wins      ", "Draws    ", "Loses    ", "Rock     ", "Paper    ", "Scissors " , "Game Time"};

            for (int i = 0; i < stats.Length; i++)
                Console.WriteLine( " " + headers[i] + " " + stats[i]);

            _menuDesign.WriteInColor(" Press any key to go back >> ", ConsoleColor.Cyan);
            Console.ReadKey();
        }

        public async Task LoadRating()
        {
            Console.Clear();
            _menuDesign.WriteHeader("rating");
            string rating = await _statisticsService.GetRating();
            _menuDesign.WriteInColor(" №  Nickname         Wins     Loses\n", ConsoleColor.Cyan);
            string[] rates = rating.Replace("\"", "").Split("|");

            for (int i = 0; i < rates.Length; i++)
            {
                if (i < 3)
                    if(i == 0)
                        _menuDesign.WriteInColor($" {i + 1}. " + rates[i] + "\n", ConsoleColor.Green);
                    else
                        _menuDesign.WriteInColor($" {i + 1}." + rates[i] + "\n", ConsoleColor.Green);
                else
                    Console.WriteLine($" {i + 1}." + rates[i]);
            }
               
            _menuDesign.WriteInColor("\n Press any key to go back >> ", ConsoleColor.Cyan);
            Console.ReadKey();
        }
    }
}
