using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AzureCosmosDbSqlApiSample.Models
{
    public class Location
    {
        [JsonProperty("id")]
        public string Id { get; set; }
        [JsonProperty("country")]
        public string Country { get; set; }
        public string  Name { get; set; }
        public string  Address { get; set; }
    }
}
