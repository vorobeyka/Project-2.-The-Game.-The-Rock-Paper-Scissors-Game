using System;
using TheRockPaperScissors.Client.Game.Enums;
using TheRockPaperScissors.Client.Models;
using TheRockPaperScissors.Client.Services;
using System.Threading.Tasks;

namespace TheRockPaperScissors.Client.Menu
{
    public class GameMenu
    {
        private readonly MenuDesign _menuDesign = new MenuDesign();
        private readonly MenuValidation _menuValidation = new MenuValidation();
        private readonly GameConnectService _gameConnectService = new GameConnectService();
        private readonly GameService _gameService = new GameService();

        public async Task Load(ConsoleColor color, User user)
        {
            Console.ForegroundColor = color;
            Console.Clear();
            _menuDesign.WriteHeader("select mode");

            var number = 1;

            foreach (GameType type in Enum.GetValues(typeof(GameType)))
            {
                Console.WriteLine($" {number} - {Enum.GetNames(typeof(GameType))[number - 1]}");
                number++;
            }

            Console.WriteLine($" {number} - Go back");
            var command = _menuValidation.CheckInteger(" Enter number >> ", number);
            try
            {
                string roomId = null;
                switch (command)
                {
                    case 1:
                        await _gameConnectService.CreateGame(user.Id, GameType.Test);
                        break;
                    case 2:
                        Console.Clear();
                        _menuDesign.WriteHeader("private mode");
                        Console.WriteLine(" 1 - Create room\n 2 - Connect to room");
                        var privateCommand = _menuValidation.CheckInteger(" Enter command >> ", 2);
                        switch (privateCommand)
                        {
                            case 1:
                                roomId = await _gameConnectService.CreateGame(user.Id, GameType.Private);
                                Console.WriteLine($" Room identifier : {roomId.Replace("\"", "")}");
                                break;
                            case 2:
                                roomId = _menuValidation.InputString(" Enter room identifier :", 4);
                                await _gameConnectService.ConnectToPrivate(user.Id, GameType.Private, roomId);
                                break;
                        }
                        break;
                    case 3:
                        _menuDesign.WriteHeader("public mode");
                        await _gameConnectService.CreateGame(user.Id, GameType.Public);
                        break;
                    case 4:
                        Console.Clear();
                        return;
                }
                Console.WriteLine(" Waiting for another player...");
                await _gameService.StartGame(user.Id);

                //MAKE MOVES
                while (true)
                {
                    await MakeMove(user.Id, roomId);
                    var message = await GetResult(user.Id);
                    if (message.Item1 == false) break;

                    _menuDesign.WriteHeader("result");
                    string[] messages = message.Item2.Replace("\"", "").Replace("|", "\n").Split('~');
                    Console.Write(messages[0]);
                    _menuDesign.WriteInColor(" " + messages[1], ConsoleColor.Cyan);
                    Console.WriteLine("\n Press ANY KEY for next round >> ");
                    Console.ReadKey();
                }

                await ShowSeriesResult(user.Id);               
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public async Task MakeMove(Guid token, string roomId)
        {
            Console.Clear();
            _menuDesign.WriteHeader("the game");
            if (roomId != null) Console.WriteLine($" Room {roomId.Replace("\"", "")}\n");
            var number = 1;
            Console.WriteLine($" You have only 20 seconds... ");

            foreach (Move m in Enum.GetValues(typeof(Move)))
            {
                Console.WriteLine($" {number} - {Enum.GetNames(typeof(Move))[number - 1]}");
                number++;
            }

            var move = _menuValidation.CheckInteger(" Make a move >> ", number - 1) - 1;
            await _gameService.StartRound(token, (Move)move);
        }

        public async Task<(bool, string)> GetResult(Guid token)
        {
            return await _gameService.GetRoundResult(token);
        }

        public async Task ShowSeriesResult(Guid token)
        {
            _menuDesign.WriteHeader("final result");
            var message = await _gameService.GetSeriesResult(token);
            var toPrint = " " + (string.IsNullOrEmpty(message)
                ? "No series has been played"
                : message.Replace("|", "\n").Replace("~", " ").Replace("\"", ""));
            Console.WriteLine(toPrint);
            Console.WriteLine("\n Press ANY KEY for return in menu");
            Console.ReadKey();
            Console.Clear();
        }
    }
}
