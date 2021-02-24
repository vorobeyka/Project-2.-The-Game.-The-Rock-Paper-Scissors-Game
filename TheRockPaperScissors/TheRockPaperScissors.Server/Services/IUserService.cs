using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Concurrent;
using TheRockPaperScissors.Server.Models;

namespace TheRockPaperScissors.Server.Services
{
    public interface IUserService
    {
        Task<Guid?> RegisterUserAsync(User user);
        Task<Guid?> LoginUserAsync(string login, string password);
    }
}
