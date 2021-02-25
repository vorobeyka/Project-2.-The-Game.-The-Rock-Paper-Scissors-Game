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
            //GameResult result = new GameResult();

            switch (command)
            {
                case 1:
                    /* result =*/ await _gameTypeService.CreateGame(user.Id, GameType.Test);
                     break;
                 case 2:
                     int privateCommand = _menuValidation.CheckInteger("\n 1 - Create room\n 2 - Connect to room", 2);
                     switch (privateCommand)
                     {
                         case 1:
                             /* result =*/
                    await _gameTypeService.CreateGame(user.Id, GameType.Private);
                            break;
                        case 2:
                            /* result =*/
                            string room = _menuValidation.InputString(" Enter room identifier :");
                            await _gameTypeService.ConnectToPrivate(user.Id, GameType.Private, room);
                            break;
                    }
                   
                    break;
                case 3:
                  /*  result = */await _gameTypeService.CreateGame(user.Id, GameType.Public);
                    break;
                case 4:
                    Console.Clear();
                    break;
            }
        }

        //public async Task/*<GameResult>*/ Play(Guid player, GameType gametype)
        //{
        //    Console.Clear();
        //    await _gameTypeService.PlayTest(player, gametype);
        //   // return new GameResult();
        //}
    }
}
