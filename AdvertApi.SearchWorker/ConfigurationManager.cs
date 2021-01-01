using System.IO;
using Microsoft.Extensions.Configuration;

namespace AdvertApi.SearchWorker
{
    internal class ConfigurationManager
    {
        private static IConfiguration _configuration = null;

        internal static IConfiguration Instance
        {
            get
            {
                if (_configuration == null)
                {
                    _configuration = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory())
                        .AddJsonFile("appsettings.json").Build();
                }

                return _configuration;
            }
        }
    }
}