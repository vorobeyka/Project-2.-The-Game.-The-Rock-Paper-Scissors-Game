using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TheRockPaperScissors.Server.Services
{
    public interface IStorage<U, T> where T : class
    {
        Task<T> GetAsync(U key);

        Task AddAsync(U key, T item);

        Task<bool> DeleteAsync(U key);
    }
}
