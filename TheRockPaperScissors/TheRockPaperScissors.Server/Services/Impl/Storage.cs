using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace TheRockPaperScissors.Server.Services.Impl
{
    public class Storage<U, T> : IStorage<U, T> where T : class
    {
        private readonly IDictionary<U, T> _db = new Dictionary<U, T>();
        private readonly SemaphoreSlim _semaphoreSlim = new SemaphoreSlim(1, 1);

        public async Task AddAsync(U key, T item)
        {
            await _semaphoreSlim.WaitAsync();
            _db[key] = item;
            _semaphoreSlim.Release();
            await Task.CompletedTask;
        }

        public async Task<bool> DeleteAsync(U key)
        {
            await _semaphoreSlim.WaitAsync();
            bool isOk = _db.ContainsKey(key);

            if (isOk) isOk = _db.Remove(key);
            else isOk = false;

            _semaphoreSlim.Release();
            return isOk;
        }

        public async Task<T> GetAsync(U key)
        {
            await _semaphoreSlim.WaitAsync();
            var item = _db.ContainsKey(key) ? _db[key] : null;
            _semaphoreSlim.Release();
            return item;
        }
    }
}
