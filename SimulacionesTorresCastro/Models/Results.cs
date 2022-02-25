using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimulacionesTorresCastro.Models
{
    public class Results
    {
        [JsonProperty(PropertyName = "id")]
        public string id { get; set; }
        [JsonProperty(PropertyName = "totalConstructedProductsMachine1")]
        public int totalConstructedProductsMachine1 { get; set; }
        [JsonProperty(PropertyName = "totalConstructedProductsMachine2")]
        public int totalConstructedProductsMachine2 { get; set; }
        [JsonProperty(PropertyName = "grossTotalProfitMachine1")]
        public double grossTotalProfitMachine1 { get; set; }
        [JsonProperty(PropertyName = "grossTotalProfitMachine2")]
        public double grossTotalProfitMachine2 { get; set; }
        [JsonProperty(PropertyName = "realSimulationProfitMachine1")]
        public double realSimulationProfitMachine1 { get; set; }
        [JsonProperty(PropertyName = "realSimulationProfitMachine2")]
        public double realSimulationProfitMachine2 { get; set; }
    }
}
