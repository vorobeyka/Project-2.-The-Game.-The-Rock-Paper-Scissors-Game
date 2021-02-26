using System;
using System.Threading.Tasks;
using TheRockPaperScissors.Client.Exceptions;
using TheRockPaperScissors.Client.Menu;

namespace TheRockPaperScissors.Client
{
    class Program
    {
        static async Task Main(string[] args)
        {
            MainMenu menu = new MainMenu();

            try
            {
                await menu.Load(ConsoleColor.Yellow);
            }
            catch (AuthorizationFailedException ex)
            {
                Console.WriteLine(ex.Message + "\n Press ANY KEY to continue...");
                Console.ReadKey();
                await Main(null);
            }
            catch (ServerNotConnectedException ex)
            {
                Console.WriteLine(ex.Message + "\n Press ANY KEY to continue...");
                Console.ReadKey();
                await Main(null);
            }
            catch (DeserializationException ex)
            {
                Console.WriteLine(ex.Message + "\n");
            }
        }
    }
}
