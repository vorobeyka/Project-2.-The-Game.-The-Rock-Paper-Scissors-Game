using System;
using System.Threading.Tasks;
using TheRockPaperScissors.Server.Models;
using TheRockPaperScissors.Server.Data;
using System.Collections;
using System.Threading;
using System.Collections.Generic;

namespace TheRockPaperScissors.Server.Services.Impl
{
    public class DatabaseService : IDatabaseService
    {
        private readonly Database _db = new Database();
        private readonly SemaphoreSlim _semaphoreSlim = new SemaphoreSlim(1, 1);

        /*public async Task<User> GetUserAsync(string login)
        {
            await _semaphoreSlim.WaitAsync();
            var user = _db.GetUser(login);
            _semaphoreSlim.Release();
            return user;
        }*/

        public IList<User> GetAllUsers()
        {
            var users = _db.GetAllUsers();
            return users;
        }

        public async Task AddUserAsync(User user)
        {
            await _semaphoreSlim.WaitAsync();
            _db.AddUser(user);
            _semaphoreSlim.Release();
        }
        
        public async Task UpdateUserAsync(User user)
        {
            await _semaphoreSlim.WaitAsync();
            _db.UpdateUser(user);
            _semaphoreSlim.Release();
        }

        public async Task<string> GetPublicStatisticsAsync()
        {
            await _semaphoreSlim.WaitAsync();
            var result = _db.GetPublicStatistics();
            _semaphoreSlim.Release();
            return result;
        }

        public async Task<string> GetUserStatisticsAsync(string login)
        {
            await _semaphoreSlim.WaitAsync();
            var result = _db.GetUserStatistics(login);
            _semaphoreSlim.Release();
            return result;
        }
    }
}
