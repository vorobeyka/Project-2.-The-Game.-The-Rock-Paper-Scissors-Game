using System;
using System.Threading.Tasks;

namespace TheRockPaperScissors.Client.Models
{
    internal interface IClientController
    {
        Task Login(string login, string password);
        Task Registration(string login, string password);
    }
}
