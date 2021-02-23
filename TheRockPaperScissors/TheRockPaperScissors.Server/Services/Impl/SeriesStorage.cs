using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TheRockPaperScissors.Server.Services.Impl
{
    public class SeriesStorage : ISeriesStorage
    {
        private readonly IList<ISeriesStorage> _series;

        Task ISeriesStorage.AddAsync(ISeriesService series)
        {
            throw new NotImplementedException();
        }

        Task ISeriesStorage.RemoveAsync(ISeriesService series)
        {
            throw new NotImplementedException();
        }
    }
}
