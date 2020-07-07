using Microsoft.Extensions.Configuration;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace WebPollyExample.Clients
{
    public class ApiClient
    {
        protected readonly HttpClient _client;
        protected readonly IConfiguration _configuration;

        public ApiClient(HttpClient client, IConfiguration configuration)
        {
            _client = client;
            _client.DefaultRequestHeaders.Accept.Clear();
            _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            _configuration = configuration;
        }

        public async Task<string> SendRequest()
        {
            var response = await _client.GetAsync(_configuration.GetSection("UrlApi").Value);
            var result = await response.Content.ReadAsStringAsync();

            if (response.IsSuccessStatusCode)
            {
                Console.ForegroundColor = ConsoleColor.DarkGreen;
                Console.WriteLine("Resultado normal: " + result);
                Console.ResetColor();
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.DarkMagenta;
                Console.WriteLine("Erro Requisicao: " + result);
                Console.ResetColor();
            }

            response.EnsureSuccessStatusCode();
            return result;
        }
    }
}
