using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace TheRockPaperScissors.Client.Models
{
    internal class ClientController : IClientController
    {
        private readonly HttpClient _client;
        private readonly string _baseAddress = "http://localhost:5000";

        public ClientController()
        {
            _client = new HttpClient()
            {
                BaseAddress = new Uri(_baseAddress)
            };
        }

        public Guid Login(string login, string password)
        {
            //TODO: try to Login
            var id = Guid.NewGuid();
//            var token = await _client.GetAsync("/user/loggin");
            return id;
        }

        public Guid Registration(string login, string password)
        {
            //TODO:: try to register
            return Guid.NewGuid();
        }
    }
}
