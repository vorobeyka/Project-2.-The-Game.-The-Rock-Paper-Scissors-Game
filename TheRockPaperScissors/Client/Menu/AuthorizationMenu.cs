using System;
using TheRockPaperScissors.Client.Models;
using TheRockPaperScissors.Client.Services;
using System.Threading.Tasks;

namespace TheRockPaperScissors.Client.Menu
{
    public class AuthorizationMenu
    {
        private readonly MenuDesign MenuDesign = new MenuDesign();
        private readonly MenuValidation MenuValidation = new MenuValidation();
        private readonly Authorization ClientController = new Authorization();

        public async Task<User> Load(ConsoleColor color)
        {
            Console.ForegroundColor = color;
            MenuDesign.WriteHeader("LET'S START!");
            Console.WriteLine(" 1 - Sign Up\n 2 - Sign In");

            int authCommand = MenuValidation.CheckInteger(" Enter command : ", 2);
            string login = MenuValidation.InputString(" Enter login : ");
            string password = MenuValidation.InputString(" Enter password : ");
            Guid token = Guid.NewGuid();

            switch (authCommand)
            {
                case 1:
                    token = await ClientController.Registration(login, password);
                    break;
                case 2:
                    token = await ClientController.Login(login, password);
                    break;
            }

            User user = new User(token, login, password);
            Console.Clear();

            return await Task.FromResult(user);
        }
    }
}
