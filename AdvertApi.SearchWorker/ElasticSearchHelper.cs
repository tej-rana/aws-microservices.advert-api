using System;
using Microsoft.Extensions.Configuration;
using Nest;

namespace AdvertApi.SearchWorker
{
    internal class ElasticSearchHelper
    {
        private static IElasticClient _client;

        public static IElasticClient GetInstance(IConfiguration config)
        {
            if (_client == null)
            {
                var url = config.GetSection("ES").GetValue<string>("url");
                var settings = new ConnectionSettings(new Uri(url)).DefaultIndex("adverts");
                _client = new ElasticClient(settings);
            }

            return _client;
        }
    }
}