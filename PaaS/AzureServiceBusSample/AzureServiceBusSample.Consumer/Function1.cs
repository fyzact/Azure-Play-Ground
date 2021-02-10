using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;

namespace AzureServiceBusSample.Consumer
{
    public static class Function1
    {
        [FunctionName("Function1")]
        public static void Run([ServiceBusTrigger("stoktransfer", Connection = "StocktransferQueue")]string myQueueItem, ILogger log)
        {
            log.LogInformation($"C# ServiceBus queue trigger function processed message: {myQueueItem}");
            
        }
    }
}
