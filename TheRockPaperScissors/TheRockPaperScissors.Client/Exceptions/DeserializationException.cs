using System;

namespace TheRockPaperScissors.Client.Exceptions
{
    public class DeserializationException : Exception
    {
        public DeserializationException() : base()
        {
        }

        public DeserializationException(string message) : base(message)
        {
        }
    }
}
