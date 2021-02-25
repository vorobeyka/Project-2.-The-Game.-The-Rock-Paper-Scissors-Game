using System;

namespace TheRockPaperScissors.Client.Exceptions
{
    public class DeserializationException : Exception
    {
        public DeserializationException(string message) : base(message)
        {
            
        }
    }
}
