using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using TheRockPaperScissors.Client.Models;
using TheRockPaperScissors.Client.Game.Enums;

namespace TheRockPaperScissors.Client.Services
{
    public class MoveService : IMoveService
    {
        public BaseService _baseService = BaseService.GetInstance();
        private readonly Serialization<MoveObject> _moveSerialization = new Serialization<MoveObject>();
        private readonly HttpClient _httpClient = BaseService.GetInstance().HttpClient;

        public async Task MakeMove(Guid token, Move move)
        {
            var json = _moveSerialization.Serialize(new MoveObject(token.ToString(), move));
            var response = await _httpClient.PostAsync($"/game/{move}/{token}",
                           new StringContent(json, Encoding.UTF8, "application/json"));
            var content = await response.Content.ReadAsStringAsync();
            Console.WriteLine(content);
        }

    }
}
