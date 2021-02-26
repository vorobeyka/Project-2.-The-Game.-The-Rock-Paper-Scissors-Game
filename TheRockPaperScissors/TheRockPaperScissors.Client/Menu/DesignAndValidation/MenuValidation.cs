using System;

namespace TheRockPaperScissors.Client.Menu
{
    public class MenuValidation
    {
        public string InputString(string message, int length)
        {
            Console.Write(message);
            string input = Console.ReadLine().Replace(" ", "").Trim();

            while (input.Length < length || input.Length > 16)
            {
                Console.Write($" Invalid input length. {length} characters minimum required." + message);
                input = Console.ReadLine().Replace(" ", "").Trim();
            }

            return input;
        }

        public int CheckInteger(string message, int limit)
        {
            Console.Write(message);
            int command;

            while (!int.TryParse(Console.ReadLine().Trim(), out command) || command < 1 || command > limit)
                Console.Write(" Invalid input." + message);

            return command;
        }
    }
}
