using System;
using System.Net.Http;
using System.Threading.Tasks;
using AdvertApi.Management.Web.Models;
using AdvertApi.Models;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

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

            var createUrl = _configuration.GetSection("AdvertApi").GetValue<string>("CreateUrl");
            _client.BaseAddress = new Uri(createUrl);
            _client.DefaultRequestHeaders.Add("Content-type", "application/json");
            
        }
        public async Task<AdvertResponse> Create(CreateAdvertModel model)
        {
            var advertApiModel = new AdvertModel(); //TODO: AutoMapper map
            var jsonModel = JsonConvert.SerializeObject(advertApiModel);
            var response = await _client.PostAsync(_client.BaseAddress, new StringContent(jsonModel));
            var responseJson = await response.Content.ReadAsStringAsync();
            var createAdvertResponse = JsonConvert.DeserializeObject<CreateAdvertResponse>(responseJson);
            var advertResponse = new AdvertResponse(); //TODO: AutoMapper map

            return advertResponse;
        }
    }
}