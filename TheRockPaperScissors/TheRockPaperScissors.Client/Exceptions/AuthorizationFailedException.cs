using System;

namespace TheRockPaperScissors.Client.Exceptions
{
    class AuthorizationFailedException : Exception
    {
        public AuthorizationFailedException() : base()
        {
        }

        public AuthorizationFailedException(string message) : base(message)
        {  
        }
    }
}
