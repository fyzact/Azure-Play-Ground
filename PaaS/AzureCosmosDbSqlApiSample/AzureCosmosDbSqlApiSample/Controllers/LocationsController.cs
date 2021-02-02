using AzureCosmosDbSqlApiSample.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Cosmos;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AzureCosmosDbSqlApiSample.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LocationsController : ControllerBase
    {
        readonly CosmosClient _cosmosClient;
        Container locationContainer;
        public LocationsController(CosmosClient cosmosClient)
        {
            _cosmosClient = cosmosClient;
            locationContainer = cosmosClient.GetContainer("tetriscdb", "tetrisLocations");
        }

        [HttpPost]
        public async Task<IActionResult> Post(Location location)
        {
            location.Id = Guid.NewGuid().ToString();
            var result = await locationContainer.CreateItemAsync(location, new PartitionKey(location.Country));
            return CreatedAtAction("Get", new { id = result.Resource.Id }, result.Resource);
        }

        [HttpGet]
        public async  Task<IActionResult> Get()
        {
            var iterator = locationContainer.GetItemQueryIterator<Location>() ;
            List<Location> locations = new List<Location>();
            while (iterator.HasMoreResults)
            {
                var pageOfLocation =await iterator.ReadNextAsync();
                locations.AddRange(pageOfLocation.ToList());
            }

            return Ok(locations);
        }
        [HttpGet("{country}")]
        public async Task<IActionResult> Get(string country)
        {
            var iterator = locationContainer.GetItemQueryIterator<Location>(requestOptions: new QueryRequestOptions
            {
                PartitionKey = new PartitionKey(country)
            });
            List<Location> locations = new List<Location>();
            while (iterator.HasMoreResults)
            {
                var pageOfLocation = await iterator.ReadNextAsync();
                locations.AddRange(pageOfLocation.ToList());
            }

            return Ok(locations);
        }
    }
}
