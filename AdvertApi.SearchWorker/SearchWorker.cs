using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using AdvertApi.Models.Messages;
using Amazon.Lambda.Core;
using Amazon.Lambda.SNSEvents;
using Nest;

[assembly:LambdaSerializer(typeof(Amazon.Lambda.Serialization.SystemTextJson.DefaultLambdaJsonSerializer))]
namespace AdvertApi.SearchWorker
{
    public class SearchWorker
    {
        private readonly IElasticClient _client;
        
        public SearchWorker(): this(ElasticSearchHelper.GetInstance(ConfigurationManager.Instance)) {}
        public SearchWorker(IElasticClient client)
        {
            _client = client;
        }
        public async Task Function(SNSEvent snsEvent, ILambdaContext context)
        {
            foreach (var record in snsEvent.Records)
            {
                context.Logger.LogLine(record.Sns.Message);
                var message = JsonSerializer.Deserialize<AdvertConfirmedMessage>(record.Sns.Message);
                var advertDocument = MappingHelper.Map(message);
                await _client.IndexDocumentAsync(advertDocument);
            }
        }
    }
}