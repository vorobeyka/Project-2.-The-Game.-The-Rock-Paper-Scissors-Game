using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace TheRockPaperScissors.Client.Services
{
    public class StatisticsService : IStatisticsService
    {
        private readonly HttpClient _httpClient = BaseService.GetInstance().HttpClient;

        public async Task<string> GetRating()
        {
            var response = await _httpClient.GetAsync($"Statistics");
            Console.WriteLine(response.StatusCode);
            return await response.Content.ReadAsStringAsync();
        }

        public async Task<string> GetStatistics(string login)
        {
            var response = await _httpClient.GetAsync($"Statistics/{login}");
            Console.WriteLine(response.StatusCode);
            return await response.Content.ReadAsStringAsync();
        }
    }
}
