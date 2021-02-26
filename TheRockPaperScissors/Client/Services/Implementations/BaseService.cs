using System;
using System.Net.Http;
using System.Net.Http.Headers;


namespace TheRockPaperScissors.Client.Services
{
    public class BaseService
    {
        public HttpClient HttpClient { get; set; }
        
        public Uri _baseAddress = new Deserialization("options/options.json").BaseAddress;

        private static BaseService _instance;

        private BaseService()
        {
            HttpClient = new HttpClient
            {
                BaseAddress = _baseAddress
            };
            HttpClient.DefaultRequestHeaders
                .Accept
                .Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public static BaseService GetInstance()
        {
            if (_instance == null)
            {
                _instance = new BaseService();
            }

            return _instance;
        }
    }
}
