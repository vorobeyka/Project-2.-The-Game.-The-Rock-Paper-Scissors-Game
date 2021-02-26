using System;
using TheRockPaperScissors.Client.Models;
using TheRockPaperScissors.Client.Services;
using System.Threading.Tasks;
using TheRockPaperScissors.Client.Exceptions;
using TheRockPaperScissors.Client.StatisticsAndRating;
using System.Net.Http;
using TheRockPaperScissors.Client.Rules;

namespace TheRockPaperScissors.Client.Menu
{
    public class AuthorizationMenu
    {
        private readonly MenuDesign _menuDesign = new MenuDesign();
        private readonly MenuValidation _menuValidation = new MenuValidation();
        private readonly Authorization _authorization = new Authorization();
        private readonly Statistics _statistics = new Statistics();
        private readonly GameRules _gameRules = new GameRules();

        public async Task<User> Load(ConsoleColor color)
        {
            Console.ForegroundColor = color;
            int command = 0;
            string login = null;
            string password = null;
            bool isLogin = false;
            Guid? token = null;

            while (command != 5 && !isLogin)
            {
                Console.Clear();
                _menuDesign.WriteHeader("let's start!");
                Console.WriteLine(" 1 - Sign Up\n 2 - Sign In\n 3 - Rating\n 4 - Rules\n 5 - Exit");
                command = _menuValidation.CheckInteger(" Enter command >> ", 5);

                try
                {
                    switch (command)
                    {
                        case 1:
                            login = _menuValidation.InputString(" Enter login >> ", 4);
                            password = _menuValidation.InputString(" Enter password >> ", 6);
                            token = await _authorization.Registration(login, password);
                            isLogin = true;
                            break;
                        case 2:
                            login = _menuValidation.InputString(" Enter login >> ", 4);
                            password = _menuValidation.InputString(" Enter password >> ", 6);
                            token = await _authorization.Login(login, password);
                            isLogin = true;
                            break;
                        case 3:
                            await _statistics.LoadRating();
                            break;
                        case 4:
                            _gameRules.LoadRules();
                            break;
                        case 5:
                            return null;
                    }
                }
                catch (HttpRequestException)
                {
                    throw new ServerNotConnectedException("\n Oops! Server is not connected...");
                }
            }

            if (token == null)
            {
                throw new AuthorizationFailedException(" Authorization failed. Try another login and/or password.");
            }
            else
            {
                Console.Clear();
                return new User((Guid)token, login, password);
            }
        }
    }
}
