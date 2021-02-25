using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using TheRockPaperScissors.Client.Models;
using TheRockPaperScissors.Client.Game.Enums;

namespace TheRockPaperScissors.Client.Services
{
    public class GameTypeService : IGameTypeService
    {
        private readonly Serialization<GameTypeObject> _gameTypeSerialization = new Serialization<GameTypeObject>();
        private readonly HttpClient _httpClient = BaseService.GetInstance().HttpClient;

        public async Task SelectGameType(Guid token, GameType gameType)
        {
            var type = Enum.GetName(typeof(GameType), gameType);
            var json = _gameTypeSerialization.Serialize(new GameTypeObject(token, gameType, null));
            var response = await _httpClient.PostAsync($"/game",
                           new StringContent(json, Encoding.UTF8, "application/json"));
            var content = await response.Content.ReadAsStringAsync();
            Console.WriteLine(content);
        }
    }
}
