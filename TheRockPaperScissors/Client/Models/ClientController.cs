using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
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
            _httpClient.DefaultRequestHeaders
                .Accept
                .Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public async Task<string> Login(string login, string password)
        {
            var response = await _httpClient.PostAsync($"Users/Login",
                                                       new StringContent(serialization.Serialize(new User(login, password)),
                                                       Encoding.UTF8,
                                                       "application/json"));
            var content = await response.Content.ReadAsStringAsync();
            return content;
        }

        public async Task<string> Registration(string login, string password)
        {
            var json = serialization.Serialize(new User(login, password));
            var response = await _httpClient.PostAsync($"Users/Register",
                                                       new StringContent(serialization.Serialize(new User(login, password)),
                                                       Encoding.UTF8,
                                                       "application/json"));
            var content = await response.Content.ReadAsStringAsync();
            return content;
        }
    }
}
