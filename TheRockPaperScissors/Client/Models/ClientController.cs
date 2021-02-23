using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace TheRockPaperScissors.Client.Models
{
    internal class ClientController : IClientController
    {
        private readonly HttpClient _httpClient = new HttpClient();
        private readonly Uri _baseAddress = new Uri("http://localhost:5000");

        public ClientController()
        {
            _httpClient.BaseAddress = _baseAddress;
        }

        public async Task Login(string login, string password)
        {
            var response = await _httpClient.GetAsync($"Users/Login?login={login}&password={password}");
            var content = await response.Content.ReadAsStringAsync();
            Console.WriteLine(content);
        }

        public async Task Registration(string login, string password)
        {
            //TODO:: try to responce
            var response = await _httpClient.GetAsync($"Users/Register?login={login}&password={password}");
            var content = await response.Content.ReadAsStringAsync();
            Console.WriteLine(content);
        }
    }
}
