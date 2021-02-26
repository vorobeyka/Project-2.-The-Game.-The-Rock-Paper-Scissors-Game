using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TheRockPaperScissors.Server.Models;

namespace TheRockPaperScissors.Server.Services
{
    public interface IUsersStorage
    {
        Task<User> GetAsync(Guid key);
        Task AddAsync(Guid key, User item);
        Task<bool> ContainAsync(Guid key);
        Task<bool> ContainValueAsync(User item);
    }
}
