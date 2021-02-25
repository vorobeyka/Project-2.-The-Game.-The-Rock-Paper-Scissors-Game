using System;

namespace TheRockPaperScissors.Client.Menu
{
    public class MenuValidation
    {
        public string InputString(string message)
        {
            Console.WriteLine(message);
            string input = Console.ReadLine();

            while (input.Trim().Length < 1)
            {
                Console.WriteLine("Invalid input. " + message);
                input = Console.ReadLine();
            }

            return input;
        }

        public int CheckInteger(string message, int limit)
        {
            Console.WriteLine(message);
            int command;

            while (!int.TryParse(Console.ReadLine().Trim(), out command) || command < 1 || command > limit)
                Console.WriteLine("Invalid input. " + message);

            return command;
        }
    }
}
