using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using AdvertApi.Management.Web.Models;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;


namespace AdvertApi.Management.Web.ServiceClients
{
    public class SearchApiClient : ISearchApiClient
    {
        private readonly HttpClient _client;
        private readonly string _baseAddress;

        public SearchApiClient(HttpClient client, IConfiguration configuration)
        {
            _client = client;
            _baseAddress = configuration.GetSection("SearchApi").GetValue<string>("url");
        }
        
        public async Task<List<AdvertType>> Search(string keyword)
        {
            var result = new List<AdvertType>();
            var callUrl = $"{_baseAddress}/search/v1/{keyword}";
            var httpResponse = await _client.GetAsync(new Uri(callUrl));

            if (httpResponse.StatusCode == HttpStatusCode.OK)
            {
                var response = await httpResponse.Content.ReadAsStringAsync();
                var allAdverts = JsonConvert.DeserializeObject<List<AdvertType>>(response);
                result.AddRange(allAdverts);
            }

            return result;
        }
    }
}