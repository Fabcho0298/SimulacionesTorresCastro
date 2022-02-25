using Microsoft.AspNetCore.Mvc;
using SimulacionesTorresCastro.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimulacionesTorresCastro.Controllers
{
    public class SimulationDetailsController : Controller
    {
        private readonly ICosmosDBServiceSimulationDetails _cosmosDbServiceSimulationDetails;
        private readonly ICosmosDBServiceProduct _cosmosDbServiceProduct;
        private readonly ICosmosDBServiceMachine _cosmosDbServiceMachine;

        public SimulationDetailsController(ICosmosDBServiceSimulationDetails cosmosDBServiceSimulationDetails, ICosmosDBServiceProduct cosmosDBServiceProduct, ICosmosDBServiceMachine cosmosDBServiceMachine)
        {
            this._cosmosDbServiceSimulationDetails = cosmosDBServiceSimulationDetails;
            this._cosmosDbServiceProduct = cosmosDBServiceProduct;
            this._cosmosDbServiceMachine = cosmosDBServiceMachine;
        }

        public async Task<ActionResult> SimulationsDetails()
        {
            return View((await _cosmosDbServiceSimulationDetails.GetSimulationsDetailsAsync("SELECT * FROM simulation")).ToList());
        }

        public async Task<ActionResult> CreateSimulation(SimulationDetails simulationDetails)
        {
            simulationDetails.id = Guid.NewGuid().ToString();

            if (simulationDetails.idMaquina1 == simulationDetails.idMaquina2 ||
                simulationDetails.productManufacturePrice < 0 ||
                simulationDetails.amountHoursPerDay < 0 ||
                simulationDetails.amountDaysPerWeek < 0 ||
                simulationDetails.amountContinuousProduction < 0)
            {
                return RedirectToAction("SimulationDetailsError");
            }
            else
            {
                await this._cosmosDbServiceSimulationDetails.AddSimulationDetailsAsync(simulationDetails);
                return RedirectToAction("Index", "Home");
            }
        }

        public IActionResult Create()
        {
            Simulation simulation = new Simulation();

            IEnumerable<Product> products = this._cosmosDbServiceProduct.GetProductsAsync("SELECT * FROM product").Result;
            simulation.simulationProducts = products;

            IEnumerable<Machine> machines = this._cosmosDbServiceMachine.GetMachinesAsync("SELECT * FROM machine").Result;
            simulation.simulationMachines = machines;

            return View(simulation);
        }

        public IActionResult SimulationDetailsError()
        {
            return View();
        }

    }
}
