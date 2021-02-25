using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TheRockPaperScissors.Server.Enums;

namespace TheRockPaperScissors.Server.Services.Impl
{
    public class SeriesStorage : ISeriesStorage
    {
        private readonly IList<ISeriesService> _seriesStorage = new List<ISeriesService>();
        private readonly SemaphoreSlim _semaphoreSlim = new SemaphoreSlim(1, 1);

        public async Task AddAsync(ISeriesService series)
        {
            await _semaphoreSlim.WaitAsync();
            _seriesStorage.Add(series);
            _semaphoreSlim.Release();
        }

        public async Task RemoveAsync(ISeriesService series)
        {
            await _semaphoreSlim.WaitAsync();
            _seriesStorage.Remove(series);
            _semaphoreSlim.Release();
        }

        public async Task RemoveAsync(Func<IEnumerable<ISeriesService>, ISeriesService> factory)
        {
            await _semaphoreSlim.WaitAsync();
            _seriesStorage.Remove(factory.Invoke(_seriesStorage));
            _semaphoreSlim.Release();
        }

        public async Task<ISeriesService> GetAsync(Func<IEnumerable<ISeriesService>, ISeriesService> factory)
        {
            await _semaphoreSlim.WaitAsync();
            var item = factory.Invoke(_seriesStorage);
            _semaphoreSlim.Release();
            return item;
        }

        public async Task<ISeriesService> GetByIdAsync(Guid id)
        {
            await _semaphoreSlim.WaitAsync();
            var item = _seriesStorage.FirstOrDefault(series => series.IsRegisteredId(id));
            _semaphoreSlim.Release();
            return item;
        }
    }
}
