using System;
using System.Collections.Generic;
using System.Text;

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
