using System;
using System.Threading.Tasks;

namespace TheRockPaperScissors.Client.Services
{
    internal interface IAuthorization
    {
        Task<Guid> Login(string login, string password);

        Task<Guid> Registration(string login, string password);
    }
}
