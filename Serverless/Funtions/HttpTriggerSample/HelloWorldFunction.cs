using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace HttpTriggerSample
{
    public static class HelloWorldFunction
    {
        [FunctionName("HelloWorld")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = "HelloWorld/{name}")] HttpRequest req,string name,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            string message = req.Query["message"];

            string requestBody = await new StreamReader(req.Body).ReadToEndAsync().ConfigureAwait(false);
            dynamic data = JsonConvert.DeserializeObject(requestBody);
            message = message ?? data?.message;

            string responseMessage = string.IsNullOrEmpty(message)
                ? $"Hello, { name}. You don't have hello world message yet. Pass a message in the query string or in the request body to your hello world message."
                : $"Hello, { name}. Your hello world message is '{message}'";
            return new OkObjectResult(responseMessage);
        }
    }
}
