using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using TheRockPaperScissors.Client.Models;
using TheRockPaperScissors.Client.Game.Enums;

namespace TheRockPaperScissors.Client.Services
{
    public class GameConnectService : IGameConnectService
    {
        private readonly Serialization<GameTypeObject> _gameTypeSerialization = new Serialization<GameTypeObject>();
        private readonly HttpClient _httpClient = BaseService.GetInstance().HttpClient;

        public async Task<string> CreateGame(Guid token, GameType gameType)
        {
            var json = _gameTypeSerialization.Serialize(new GameTypeObject(token.ToString(), gameType, null));
            var response = await _httpClient.PostAsync($"Game",
                           new StringContent(json, Encoding.UTF8, "application/json"));

            Console.WriteLine(" Waiting for another player...");
            return await response.Content.ReadAsStringAsync();
        }

        public async Task<string> ConnectToPrivate(Guid token, GameType gameType, string id)
        {
            var json = _gameTypeSerialization.Serialize(new GameTypeObject(token.ToString(), gameType, id));
            var response = await _httpClient.PostAsync($"Game",
                           new StringContent(json, Encoding.UTF8, "application/json"));
            var content = await response.Content.ReadAsStringAsync();

            return response.IsSuccessStatusCode ? content.Replace("\"", "") : throw new Exception(content);
        }
    }
}
