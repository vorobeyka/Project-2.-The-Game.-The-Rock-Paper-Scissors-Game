using System.Threading.Tasks;

namespace TheRockPaperScissors.Client.Services
{
    public interface IStatisticsService
    {
        public Task<string> GetRating();

        public Task<string> GetStatistics(string login);
    }
}
