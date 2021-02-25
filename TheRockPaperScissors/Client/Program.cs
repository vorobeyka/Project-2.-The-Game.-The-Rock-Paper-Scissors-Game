using System;
using System.Net.Http;
using System.Threading.Tasks;
using TheRockPaperScissors.Client.Exceptions;
using TheRockPaperScissors.Client.Menu;

namespace TheRockPaperScissors.Client
{
    class Program
    {
        static async Task Main(string[] args)
        {
            try
            {
                MainMenu menu = new MainMenu();
                await menu.Load(ConsoleColor.Yellow);
            }
            catch (AuthorizationFailedException ex)
            {
                Console.WriteLine(ex.Message + "\n");
            }
            catch (HttpRequestException)
            {
                Console.WriteLine("\n Oops! Server is not connected...");
            }
            catch (DeserializationException ex)
            {
                Console.WriteLine(ex.Message + "\n");
            }
        }
    }
}
