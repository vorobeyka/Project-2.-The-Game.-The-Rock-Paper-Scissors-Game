using System;
using TheRockPaperScissors.Client.Models;
using TheRockPaperScissors.Client.Services;
using System.Threading.Tasks;

namespace TheRockPaperScissors.Client.Menu
{
    public class AuthorizationMenu
    {
        private readonly MenuDesign _menuDesign = new MenuDesign();
        private readonly MenuValidation _menuValidation = new MenuValidation();
        private readonly Authorization _authorization = new Authorization();

        public async Task<User> Load(ConsoleColor color)
        {
            Console.ForegroundColor = color;
            _menuDesign.WriteHeader("LET'S START!");
            Console.WriteLine(" 1 - Sign Up\n 2 - Sign In");

            int command = _menuValidation.CheckInteger(" Enter command : ", 2);
            string login = _menuValidation.InputString(" Enter login : ");
            string password = _menuValidation.InputString(" Enter password : ");
            Guid? token = Guid.NewGuid();

            switch (command)
            {
                case 1:
                    token = await _authorization.Registration(login, password);
                    break;
                case 2:
                    token = await _authorization.Login(login, password);
                    break;
            }

            if (token == null)
            {
                Console.WriteLine(" Authorization failed.");
                return null;
            }
            else
            {
                Console.Clear();
                return new User((Guid)token, login, password); ;
            }
        }
    }
}
