using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Threading;
using TheRockPaperScissors.Server.Models;

namespace TheRockPaperScissors.Server.Services.Impl
{
    public class UserService : IUserService
    {
        private readonly IList<User> _users = new List<User>();
        private readonly SemaphoreSlim _semaphoreSlim = new SemaphoreSlim(1, 1);

        public async Task<Guid?> LoginUserAsync(string login, string password)
        {
            await _semaphoreSlim.WaitAsync();
            var user = _users.FirstOrDefault(user => user.Login == login && user.Password == password);
            _semaphoreSlim.Release();

            if (user == null) return null;

            return Guid.NewGuid();
        }

        public async Task<Guid?> RegisterUserAsync(User user)
        {
            await _semaphoreSlim.WaitAsync();
            if (_users.FirstOrDefault(u => u.Login == user.Login) != null)
            {
                _semaphoreSlim.Release();
                return null;
            }
            else
            {
                _users.Add(user);
                _semaphoreSlim.Release();
            }
            return Guid.NewGuid();
        }
    }
}
