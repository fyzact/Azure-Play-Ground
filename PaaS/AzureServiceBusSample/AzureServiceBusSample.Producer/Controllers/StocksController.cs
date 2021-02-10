using AzureServiceBusSample.Producer.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.ServiceBus;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AzureServiceBusSample.Producer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StocksController : ControllerBase
    {
        IConfiguration _configuration;
        public StocksController(IConfiguration configuration)
        {
            _configuration = configuration;
        }


        // POST api/<StocksController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CreateStock stock)
        {

            QueueClient queueClient = new QueueClient(connectionString: _configuration.GetConnectionString("serviceBus"), entityPath: "stoktransfer");

            var jsonStock = JsonConvert.SerializeObject(stock);
            Message message = new Message(System.Text.Encoding.UTF8.GetBytes(jsonStock));
            await queueClient.SendAsync(message);
            return Ok(); //It should  created
        }



    }
}
