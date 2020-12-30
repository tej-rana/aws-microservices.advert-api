using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using AdvertApi.Management.Web.Models;
using AdvertApi.Models;
using AutoMapper;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

namespace AdvertApi.Management.Web.ServiceClients
{
    public class AdvertApiClient : IAdvertApiClient
    {
        private readonly HttpClient _client;
        private readonly IMapper _mapper;

        public AdvertApiClient(IConfiguration configuration, HttpClient client, IMapper mapper)
        {
            _client = client;
            _mapper = mapper;

            var createUrl = configuration.GetSection("AdvertApi").GetValue<string>("CreateUrl");
            _client.BaseAddress = new Uri(createUrl);
            _client.DefaultRequestHeaders.Add("Content-type", "application/json");
            
        }
        public async Task<AdvertResponse> CreateAsync(CreateAdvertModel model)
        {
            var advertApiModel = _mapper.Map<AdvertModel>(model);
            var jsonModel = JsonConvert.SerializeObject(advertApiModel);
            var response = await _client.PostAsync(new Uri($"{_client.BaseAddress}/create"), new StringContent(jsonModel));
            var responseJson = await response.Content.ReadAsStringAsync();
            var createAdvertResponse = JsonConvert.DeserializeObject<CreateAdvertResponse>(responseJson);
            var advertResponse = _mapper.Map<AdvertResponse>(createAdvertResponse); 

            return advertResponse;
        }

        public async Task<bool> ConfirmAsync(ConfirmAdvertRequest model)
        {
            var confirmApiModel = _mapper.Map<ConfirmAdvertModel>(model);
            var jsonModel = JsonConvert.SerializeObject(confirmApiModel);
            var response =
                await _client.PutAsync(new Uri($"{_client.BaseAddress}/confirm"), new StringContent(jsonModel));

            return response.StatusCode == HttpStatusCode.OK;
        }
    }
}