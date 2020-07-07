using Microsoft.Extensions.Configuration;
using System.Net.Http;

namespace WebPollyExample.Clients
{
    public class WaitAndRetryApiClient : ApiClient
    {
        public WaitAndRetryApiClient(HttpClient client, IConfiguration configuration)
            : base(client, configuration)
        { }
    }
}