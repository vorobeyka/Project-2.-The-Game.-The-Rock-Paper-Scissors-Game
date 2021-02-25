using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TheRockPaperScissors.Server.Models;

namespace TheRockPaperScissors.Server.Services
{
    public interface IDatabaseService
    {
        IList<User> GetAllUsers();
        Task<User> GetUserAsync(string login);
        Task<string> GetPublicStatisticsAsync();
        Task<string> GetUserStatisticsAsync(string login);
        Task AddUserAsync(User user);
        Task UpdateUserAsync(User user);
    }
}
