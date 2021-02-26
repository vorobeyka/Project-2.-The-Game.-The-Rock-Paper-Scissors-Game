using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TheRockPaperScissors.Server.Models;

namespace TheRockPaperScissors.Server.Services.Impl
{
    public class UsersStorage : IUsersStorage
    {
        private readonly IDictionary<Guid, User> _storage = new Dictionary<Guid, User>();
        private readonly SemaphoreSlim _semaphoreSlim = new SemaphoreSlim(1, 1);

        public async Task AddAsync(Guid key, User item)
        {
            await _semaphoreSlim.WaitAsync();
            _storage[key] = item;
            _semaphoreSlim.Release();
            await Task.CompletedTask;
        }

        public async Task<User> GetAsync(Guid key)
        {
            await _semaphoreSlim.WaitAsync();
            var item = _storage.ContainsKey(key) ? _storage[key] : null;
            _semaphoreSlim.Release();
            return item;
        }

        public async Task<bool> ContainAsync(Guid key)
        {
            await _semaphoreSlim.WaitAsync();
            var result = _storage.ContainsKey(key);
            _semaphoreSlim.Release();
            return result;
        }

        public async Task<bool> ContainValueAsync(User item)
        {
            await _semaphoreSlim.WaitAsync();
            var result = _storage.Any(storageItem => storageItem.Value.Login == item.Login);
            _semaphoreSlim.Release();
            return result;
        }
    }
}
