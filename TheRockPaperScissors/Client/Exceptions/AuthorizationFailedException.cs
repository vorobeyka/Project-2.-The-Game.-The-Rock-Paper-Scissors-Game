using System;

namespace TheRockPaperScissors.Client.Exceptions
{
    class AuthorizationFailedException : Exception
    {
        public AuthorizationFailedException(string message) : base(message)
        { 
            
        }
    }
}
