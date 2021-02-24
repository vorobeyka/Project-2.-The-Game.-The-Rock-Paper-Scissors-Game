using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace TheRockPaperScissors.Server.Services.Impl
{
    public class Storage<U, T> : IStorage<U, T> where T : class
    {
        private readonly IDictionary<U, T> _storage = new Dictionary<U, T>();
        private readonly SemaphoreSlim _semaphoreSlim = new SemaphoreSlim(1, 1);

        public async Task AddAsync(U key, T item)
        {
            await _semaphoreSlim.WaitAsync();
            _storage[key] = item;
            _semaphoreSlim.Release();
            await Task.CompletedTask;
        }

        public async Task<bool> DeleteAsync(U key)
        {
            await _semaphoreSlim.WaitAsync();
            bool isOk = _storage.ContainsKey(key);

            if (isOk) isOk = _storage.Remove(key);
            else isOk = false;

            _semaphoreSlim.Release();
            return isOk;
        }

        public async Task<T> GetAsync(U key)
        {
            await _semaphoreSlim.WaitAsync();
            var item = _storage.ContainsKey(key) ? _storage[key] : null;
            _semaphoreSlim.Release();
            return item;
        }

        public async Task<bool> ContainAsync(U key)
        {
            await _semaphoreSlim.WaitAsync();
            var result = _storage.ContainsKey(key);
            _semaphoreSlim.Release();
            return result;
        }
    }
}
