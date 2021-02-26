using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using TheRockPaperScissors.Client.Models;

namespace TheRockPaperScissors.Client.Services
{
    public class Authorization : IAuthorization
    {
        private readonly Serialization<User> _userSerialization = new Serialization<User>();
        private readonly HttpClient _httpClient = BaseService.GetInstance().HttpClient;

        public async Task<Guid?> Login(string login, string password)
        {
            var json = _userSerialization.Serialize(new User(login, password));
            var response = await _httpClient.PostAsync($"Users/Login",
                            new StringContent(json, Encoding.UTF8, "application/json"));
            var content = await response.Content.ReadAsStringAsync();

            if (response.IsSuccessStatusCode)
                return Guid.Parse(content.Replace("\"", ""));
            else
                return null;
        }

        public async Task<Guid?> Registration(string login, string password)
        {
            var json = _userSerialization.Serialize(new User(login, password));
            var response = await _httpClient.PostAsync($"Users/Register",
                           new StringContent(json, Encoding.UTF8, "application/json"));
            var content = await response.Content.ReadAsStringAsync();

            if (response.IsSuccessStatusCode)
                return Guid.Parse(content.Replace("\"", ""));
            else 
                return null;
        }
    }
}