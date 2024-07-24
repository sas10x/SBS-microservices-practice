
using Core.Entities;
using Core.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Net.Http.Json;
using System;
using System.Text.Json;

namespace Infrastructure.Services
{
    public class AuthService : IAuthService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ILogger _logger;
        private readonly string _apiUrl;
        public AuthService(IHttpClientFactory httpClientFactory, ILogger<AuthService> logger, IConfiguration configuration)
        {
            _httpClientFactory = httpClientFactory;
            _logger = logger;
            _apiUrl = configuration["IdentityUrl"]; 
        }
       
        public async Task<User> GetUser(string token)
        {
            using (var httpClient = _httpClientFactory.CreateClient())
            {
                var request = new HttpRequestMessage
                {
                    Method = HttpMethod.Get,
                    RequestUri = new Uri($"{_apiUrl}"),
                    Headers =
                    {
                        { "Authorization", token },
                        { "accept", "application/json" }
                    },
                    Content = JsonContent.Create(new {})
                };   

                string jsonString = System.Text.Json.JsonSerializer.Serialize(request);
                Console.WriteLine(jsonString);

                using (var response = await httpClient.SendAsync(request))
                {
                    response.EnsureSuccessStatusCode();
                    var body = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<User>(body);
                }
            }
        }
    }
}