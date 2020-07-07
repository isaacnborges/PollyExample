using Microsoft.Extensions.Configuration;
using System.Net.Http;

namespace WebPollyExample.Clients
{
    public class CircuitBreakerApiClient : ApiClient
    {
        public CircuitBreakerApiClient(HttpClient client, IConfiguration configuration) 
            : base(client, configuration)
        { }
    }
}