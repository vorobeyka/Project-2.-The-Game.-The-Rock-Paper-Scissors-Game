using System;
using TheRockPaperScissors.Client.Models;
using TheRockPaperScissors.Client.Services;
using System.Threading.Tasks;
using TheRockPaperScissors.Client.Exceptions;
using TheRockPaperScissors.Client.StatisticsAndRating;

namespace TheRockPaperScissors.Client.Menu
{
    public class AuthorizationMenu
    {
        private readonly MenuDesign _menuDesign = new MenuDesign();
        private readonly MenuValidation _menuValidation = new MenuValidation();
        private readonly Authorization _authorization = new Authorization();
        private readonly Statistics _statistics = new Statistics();

        public async Task<User> Load(ConsoleColor color)
        {
            Console.ForegroundColor = color;
            int command = 0;
            string login = null;
            string password = null;
            bool isLogin = false;
            Guid? token = null;

            while (command != 4 && !isLogin)
            {
                Console.Clear();
                _menuDesign.WriteHeader("let's start!");
                Console.WriteLine(" 1 - Sign Up\n 2 - Sign In\n 3 - Rating\n 4 - Exit");
                command = _menuValidation.CheckInteger(" Enter command >> ", 4);
                
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
                        return null;
                }
            }

            if (token == null)
            {
                throw new AuthorizationFailedException(" Authorization failed.");
            }
            else
            {
                Console.Clear();
                return new User((Guid)token, login, password);
            }
        }
    }
}
