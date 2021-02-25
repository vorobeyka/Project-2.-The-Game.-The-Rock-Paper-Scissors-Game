using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TheRockPaperScissors.Server.Services
{
    public interface ISeriesStorage
    {
        Task AddAsync(ISeriesService series);
        Task RemoveAsync(ISeriesService series);
        Task RemoveAsync(Func<IEnumerable<ISeriesService>, ISeriesService> factory);
        Task<ISeriesService> GetAsync(Func<IEnumerable<ISeriesService>, ISeriesService> factory);
        Task<ISeriesService> GetByIdAsync(Guid id);
    }
}
