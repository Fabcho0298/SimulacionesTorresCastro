using Microsoft.AspNetCore.Mvc;
using SimulacionesTorresCastro.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimulacionesTorresCastro.Controllers
{
    public class SimulationExecutionController : Controller
    {

        private readonly ICosmosDBServiceSimulationDetails _cosmosDbServiceSimulationDetails;

        public SimulationExecutionController(ICosmosDBServiceSimulationDetails cosmosDbServiceSimulationDetails)
        {
            _cosmosDbServiceSimulationDetails = cosmosDbServiceSimulationDetails;
        }

        public IActionResult SimulationExecution()
        {

            IEnumerable<SimulationDetails> simulationDetails = this._cosmosDbServiceSimulationDetails.GetSimulationsDetailsAsync("SELECT * FROM simulation").Result;

            return View();
        }
    }
}
