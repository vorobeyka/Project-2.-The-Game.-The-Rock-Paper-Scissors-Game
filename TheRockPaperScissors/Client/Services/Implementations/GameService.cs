using System;
using TheRockPaperScissors.Client.Models;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using TheRockPaperScissors.Client.Game.Enums;
using System.Net;

namespace TheRockPaperScissors.Client.Services
{
    public class GameService : IGameService
    {
        private readonly HttpClient _httpClient = BaseService.GetInstance().HttpClient;
        private readonly Serialization<MoveObject> _roundSerialization = new Serialization<MoveObject>();

        public async Task<string> StartGame(Guid token)
        {
            var response = await _httpClient.GetAsync($"Game/start/{token}");

            return await response.Content.ReadAsStringAsync();
        }

        public async Task<string> StartRound(Guid token, Move move)
        {
            var json = _roundSerialization.Serialize(new MoveObject(token.ToString(), move));
            var response = await _httpClient.PostAsync($"Game/Round",
                           new StringContent(json, Encoding.UTF8, "application/json"));

            return await response.Content.ReadAsStringAsync();
        }

        public async Task<(bool, string)> GetRoundResult(Guid token)
        {
            var response = await _httpClient.GetAsync($"Game/roundResult/{token}");
            var content = await response.Content.ReadAsStringAsync();

            if (response.StatusCode == HttpStatusCode.NotFound)
                return (false, content);

            return (true, content);
        }

        public async Task<string> GetSeriesResult(Guid token)
        {
            var response = await _httpClient.GetAsync($"Game/seriesResult/{token}");
            return await response.Content.ReadAsStringAsync();
        }
    }
}
