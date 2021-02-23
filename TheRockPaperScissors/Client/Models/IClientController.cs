using System;
using System.Threading.Tasks;

namespace TheRockPaperScissors.Client.Models
{
    internal interface IClientController
    {
        Task<string> Login(string login, string password);

        Task<string> Registration(string login, string password);
    }
}
