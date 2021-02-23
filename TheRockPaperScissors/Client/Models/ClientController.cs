using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using TheRockPaperScissors.Client.Services;

namespace TheRockPaperScissors.Client.Models
{
    internal class ClientController : IClientController
    {
        private readonly HttpClient _httpClient = new HttpClient();
        private readonly Uri _baseAddress = new Uri("http://localhost:5000");
        private readonly Serialization<User> serialization = new Serialization<User>();

        public ClientController()
        {
            _httpClient.BaseAddress = _baseAddress;
        }

        public async Task<string> Login(string login, string password)
        {
            var response = await _httpClient.PostAsync($"Users/Login", new StringContent(serialization.Serialize(new User(login, password))));
            var content = await response.Content.ReadAsStringAsync();
            return content;
        }

        public async Task<string> Registration(string login, string password)
        {
            var response = await _httpClient.PostAsync($"Users/Register", new StringContent(serialization.Serialize(new User(login, password))));
            var content = await response.Content.ReadAsStringAsync();
            return content;
        }
    }
}
