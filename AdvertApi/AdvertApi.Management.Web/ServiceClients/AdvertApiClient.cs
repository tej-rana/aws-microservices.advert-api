using System.Net.Http;
using System.Threading.Tasks;
using AdvertApi.Management.Web.Models;
using Microsoft.Extensions.Configuration;

namespace AdvertApi.Management.Web.ServiceClients
{
    public class AdvertApiClient : IAdvertApiClient
    {
        private readonly IConfiguration _configuration;
        private readonly HttpClient _client;

        public AdvertApiClient(IConfiguration configuration, HttpClient client)
        {
            _configuration = configuration;
            _client = client;
        }
        public async Task<AdvertResponse> Create(CreateAdvertModel model)
        {
            throw new System.NotImplementedException();
        }
    }
}