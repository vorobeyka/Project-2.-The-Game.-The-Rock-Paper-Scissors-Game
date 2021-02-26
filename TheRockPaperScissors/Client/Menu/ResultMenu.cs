using System;
using System.Threading.Tasks;
using TheRockPaperScissors.Client.Services;

namespace TheRockPaperScissors.Client.Menu
{
    class ResultMenu
    {
        private readonly MenuDesign _menuDesign = new MenuDesign();
        private readonly GameService _gameService = new GameService();

        public async Task<(bool, string)> GetResult(Guid token)
        {
            return await _gameService.GetRoundResult(token);
        }

        public async Task ShowSeriesResult(Guid token)
        {
            _menuDesign.WriteHeader("final result");
            var message = await _gameService.GetSeriesResult(token);
            var toPrint = (string.IsNullOrEmpty(message)
                ? "No series has been played"
                : message.Replace("|", "\n").Replace("~", " ").Replace("\"", ""));
            Console.WriteLine(toPrint);
            Console.WriteLine("\n Press ANY KEY for return to menu >> ");
            Console.ReadKey();
            Console.Clear();
        }
    }
}
