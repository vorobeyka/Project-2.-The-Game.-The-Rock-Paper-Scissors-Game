using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using TheRockPaperScissors.Client.Models;

namespace TheRockPaperScissors.Client.Services
{
    internal class Authorization : IAuthorization
    {
        private readonly HttpClient _httpClient = new HttpClient();
        private readonly Uri _baseAddress = new Uri("http://localhost:5000");
        private readonly Serialization<User> serialization = new Serialization<User>();

        public Authorization()
        {
            _httpClient.BaseAddress = _baseAddress;
            _httpClient.DefaultRequestHeaders
                .Accept
                .Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public async Task<Guid> Login(string login, string password)
        {
            var json = serialization.Serialize(new User(login, password));
            var response = await _httpClient.PostAsync($"Users/Login",
                                                       new StringContent(json,
                                                       Encoding.UTF8,
                                                       "application/json"));
            var content = await response.Content.ReadAsStringAsync();
            return Guid.Parse(content);
        }

        public async Task<Guid> Registration(string login, string password)
        {
            var json = serialization.Serialize(new User(login, password));
            var response = await _httpClient.PostAsync($"Users/Register",
                                                       new StringContent(json,
                                                       Encoding.UTF8,
                                                       "application/json"));
            var content = await response.Content.ReadAsStringAsync();
            return Guid.Parse(content.Replace("\"", ""));
        }
    }
}