using Amazon.Lambda.Core;
using Amazon.Lambda.SNSEvents;

[assembly:LambdaSerializer(typeof(Amazon.Lambda.Serialization.Json.JsonSerializer))]
namespace AdvertApi.SearchWorker
{
    public class SearchWorker
    {
        public void Function(SNSEvent snsEvent, ILambdaContext context)
        {
            foreach (var record in snsEvent.Records)
            {
                context.Logger.LogLine(record.Sns.Message);
            }
        }
    }
}