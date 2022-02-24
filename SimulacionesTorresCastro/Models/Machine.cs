using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimulacionesTorresCastro.Models
{
    public class Machine
    {
        [JsonProperty(PropertyName = "id")]
        public int id { get; set; }

        //[JsonProperty(PropertyName = "product")]
        //public Product product { get; set; }

        [JsonProperty(PropertyName = "amountProductManufacturedPerHour")]
        public double amountProductManufacturedPerHour { get; set; }

        [JsonProperty(PropertyName = "costPerHour")]
        public double costPerHour { get; set; }

        [JsonProperty(PropertyName = "probabilityToFail")]
        public double probabilityToFail { get; set; }

        [JsonProperty(PropertyName = "amountHoursToBeFixed")]
        public double amountHoursToBeFixed { get; set; }

        [JsonProperty(PropertyName = "functionalState")]
        public string functionalState { get; set; }

        [JsonProperty(PropertyName = "purchaseDate")]
        public DateTime purchaseDate { get; set; }
    }
}
