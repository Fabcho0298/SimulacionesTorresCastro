using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimulacionesTorresCastro.Models
{
    public class SimulationDetails
    {
        [JsonProperty(PropertyName = "id")]
        public string id { get; set; }
        [JsonProperty(PropertyName = "idMaquina1")]
        public string idMaquina1 { get; set; }
        [JsonProperty(PropertyName = "idMaquina2")]
        public string idMaquina2 { get; set; }
        [JsonProperty(PropertyName = "amountHoursPerDay")]
        public int amountHoursPerDay { get; set; }
        [JsonProperty(PropertyName = "amountDaysPerWeek")]
        public int amountDaysPerWeek { get; set; }
        [JsonProperty(PropertyName = "productManufacturePrice")]
        public int productManufacturePrice { get; set; }
        [JsonProperty(PropertyName = "amountContinuousProduction")]
        public int amountContinuousProduction { get; set; }
        [JsonProperty(PropertyName = "product")]
        public string product { get; set; }
        [JsonProperty(PropertyName = "typeProduction")]
        public string typeProduction { get; set; }

    }
}
