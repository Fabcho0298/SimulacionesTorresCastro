using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimulacionesTorresCastro.Models
{
    public class Product
    {
        [JsonProperty(PropertyName = "id")]
        public string id { get; set; }

        [JsonProperty(PropertyName = "price")]
        public double price { get; set; }

        [JsonProperty(PropertyName = "name")]
        public string name { get; set; }
    }
}
