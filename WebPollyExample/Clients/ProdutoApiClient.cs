using Microsoft.Extensions.Configuration;
using System.Net.Http;

namespace WebPollyExample.Clients
{
    public class ProdutoApiClient : ApiClient
    {
        public ProdutoApiClient(HttpClient client, IConfiguration configuration)
            : base(client, configuration)
        { }
    }
}