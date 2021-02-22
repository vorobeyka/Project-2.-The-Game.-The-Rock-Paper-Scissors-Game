using System;

namespace TheRockPaperScissors.Client.Models
{
    internal interface IClientController
    {
        Guid Login(string login, string password);
        Guid Registration(string login, string password);
    }
}
