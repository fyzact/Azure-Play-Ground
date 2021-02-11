using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace AzureServiceBusSample.Consumer
{
    public static class Function1
    {
        [FunctionName("Function1")]
        public static void Run([ServiceBusTrigger("stoktransfer", Connection = "StocktransferQueue")]string myQueueItem,  [CosmosDB(databaseName:"tetriscdb",collectionName:"stock", ConnectionStringSetting= "stockDb")]out dynamic document,ILogger log)
        {
            var dynamic = JsonConvert.DeserializeObject<dynamic>(myQueueItem);
            document = new
            {
                id = System.Guid.NewGuid().ToString(),
                category="stock",
                productName=dynamic.Product,
                quantity=dynamic.Quantity,
                price=dynamic.Price
            };
            log.LogInformation($"C# ServiceBus queue trigger function processed message: {myQueueItem}");
            
        }
    }
}
