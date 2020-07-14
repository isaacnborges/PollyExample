using Microsoft.Extensions.Configuration;
using Polly;
using Polly.CircuitBreaker;
using System;
using System.Net.Http;
using System.Threading;

namespace ConsoleRequest
{
    class Program
    {
        protected Program()
        { }

        private static IConfiguration _configuration;

        static void Main(string[] args)
        {
            _configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();

            Console.WriteLine("Polly Example");
            Console.WriteLine("Circuit Breaker");
            CircuitBreaker();
        }

        private static void CircuitBreaker()
        {
            var circuitBreakerPolicy = Policy
                .Handle<Exception>()
                .CircuitBreaker(3, TimeSpan.FromSeconds(15));

            var policy = Policy
                .Handle<Exception>()
                .Fallback(() => GetFallbackCache())
                .Wrap(circuitBreakerPolicy);

            for (int i = 1; i <= 20; i++)
            {
                Console.WriteLine();
                Console.WriteLine($"Requisição {i}");
                Thread.Sleep(TimeSpan.FromSeconds(3));

                Console.ForegroundColor = circuitBreakerPolicy.CircuitState switch
                {
                    CircuitState.Closed => ConsoleColor.Green,
                    CircuitState.Open => ConsoleColor.Red,
                    CircuitState.HalfOpen => ConsoleColor.DarkYellow,
                    _ => ConsoleColor.White,
                };

                Console.WriteLine($"Estado do circuito {circuitBreakerPolicy.CircuitState}");

                policy.Execute(() =>
                {
                    SendRequest();
                });
            }
        }

        static void GetFallbackCache()
        {
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine($"Lendo do cache");
            Console.ResetColor();
        }

        static void SendRequest()
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("SEND REQUEST");
            Console.ResetColor();

            var result = new HttpClient().GetAsync(_configuration.GetSection("UrlApi").Value).Result;

            Console.WriteLine($"Response: {result.Content.ReadAsStringAsync().Result}");
        }
    }
}
