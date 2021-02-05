using Azure.Storage.Queues;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AzureStorageQueueSample.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {


        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }

        [HttpPost]
        public IActionResult Post()
        {

            QueueClient queueClient = new QueueClient("connectionstring", "locations");
            var serializeObject = JsonConvert.SerializeObject(new
            {
                country = "Uk",
                Address = "Kingston",
                Location = "London"
            });
            var byteArray = System.Text.Encoding.UTF8.GetBytes(serializeObject);
            var response = queueClient.SendMessage(Convert.ToBase64String(byteArray));

            return Ok("ok"); //Note: it shouldbe CreatedAtAction or Created
        }
    }
}
