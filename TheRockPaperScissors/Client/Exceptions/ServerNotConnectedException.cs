using System;

namespace TheRockPaperScissors.Client.Exceptions
{
    public class ServerNotConnectedException : Exception
    {
        public ServerNotConnectedException() : base()
        { 
        }
        
        public ServerNotConnectedException(string message) : base(message)
        { 
        }
    }
}
