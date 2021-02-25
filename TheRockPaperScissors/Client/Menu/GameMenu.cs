using System;
using TheRockPaperScissors.Client.Game.Enums;
using TheRockPaperScissors.Client.Game;
using TheRockPaperScissors.Client.Models;
using TheRockPaperScissors.Client.Services;
using System.Threading.Tasks;

namespace TheRockPaperScissors.Client.Menu
{
    public class GameMenu
    {
        private readonly MenuDesign _menuDesign = new MenuDesign();
        private readonly MenuValidation _menuValidation = new MenuValidation();
        //private readonly GameModes _gameModes = new GameModes();
        private readonly GameTypeService _gameTypeService = new GameTypeService();

        public async Task Load(ConsoleColor color, User user)
        {
            Console.ForegroundColor = color;
            Console.Clear();
            _menuDesign.WriteHeader("SELECT MODE");

            int number = 1;

            foreach (GameType type in Enum.GetValues(typeof(GameType)))
            {
                Console.WriteLine($" {number} - {Enum.GetNames(typeof(GameType))[number-1]}");
                number++;
            }

            Console.WriteLine($" {number} - Exit");
            int command = _menuValidation.CheckInteger(" Enter number ", number);
            GameResult result = new GameResult();

            switch (command)
            {
                case 1:
                    result = await Play(user.Id, GameType.Training);
                    break;
                case 2:
                    result = await Play(user.Id, GameType.Private);
                    throw new NotImplementedException();
                case 3:
                    result = await Play(user.Id, GameType.Public);
                    throw new NotImplementedException();
                case 4:
                    Console.Clear();
                    break;
            }
            Console.WriteLine(result.Result);
        }

        public async Task<GameResult> Play(Guid player, GameType gametype)
        {
            Console.Clear();
            await _gameTypeService.SelectGameType(player, gametype);
            throw new NotImplementedException();
        }
    }
}
