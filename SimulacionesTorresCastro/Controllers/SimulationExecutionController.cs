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
        private readonly ICosmosDBServiceProduct _cosmosDbServiceProduct;
        private readonly ICosmosDBServiceMachine _cosmosDbServiceMachine;
        private readonly ICosmosDBServiceResults _cosmosDbServiceResults;

        public SimulationExecutionController(ICosmosDBServiceSimulationDetails cosmosDbServiceSimulationDetails, ICosmosDBServiceProduct cosmosDBServiceProduct, ICosmosDBServiceMachine cosmosDBServiceMachine, ICosmosDBServiceResults cosmosDBServiceResults)
        {
            this._cosmosDbServiceSimulationDetails = cosmosDbServiceSimulationDetails;
            this._cosmosDbServiceProduct = cosmosDBServiceProduct;
            this._cosmosDbServiceMachine = cosmosDBServiceMachine;
            this._cosmosDbServiceResults = cosmosDBServiceResults;
        }

        public IActionResult SimulationExecutionError()
        {
            return View();
        }

        public IActionResult SimulationExecution()
        {

            try
            {
                IEnumerable<SimulationDetails> simulationDetails = this._cosmosDbServiceSimulationDetails.GetSimulationsDetailsAsync("SELECT * FROM simulation").Result;

                var lastSimulationDetails = simulationDetails.ToList().Last();

                #region Real machines hours to production

                int countDayOfWeek = 0;
                int realDaysProduction = 0;
                int realHoursProduction = 0;


                if (lastSimulationDetails.typeProduction == "Day")
                {

                    for (int i = 1; i <= lastSimulationDetails.amountContinuousProduction; i++)
                    {
                        if (countDayOfWeek == 7)
                        {
                            countDayOfWeek = 0;
                        }
                        else
                        {
                            if (countDayOfWeek < lastSimulationDetails.amountDaysPerWeek)
                            {
                                realDaysProduction++;
                            }
                            countDayOfWeek++;
                        }
                    }

                    realHoursProduction = realDaysProduction * lastSimulationDetails.amountHoursPerDay;

                }

                else
                {
                    int simulationDays = lastSimulationDetails.typeProduction == "Week"
                                            ? lastSimulationDetails.amountContinuousProduction * 7
                                            : (int)(lastSimulationDetails.amountContinuousProduction * 30.417);

                    for (int i = 1; i <= simulationDays; i++)
                    {
                        if (countDayOfWeek == 7)
                        {
                            countDayOfWeek = 0;
                        }
                        else
                        {
                            if (countDayOfWeek < lastSimulationDetails.amountDaysPerWeek)
                            {
                                realDaysProduction++;
                            }
                            countDayOfWeek++;
                        }
                    }

                    realHoursProduction = realDaysProduction * lastSimulationDetails.amountHoursPerDay;
                }

                //Results calculation

                Machine machineOne = this._cosmosDbServiceMachine.GetMachineAsync(lastSimulationDetails.idMaquina1).Result;
                Machine machineTwo = this._cosmosDbServiceMachine.GetMachineAsync(lastSimulationDetails.idMaquina2).Result;

                Product product = this._cosmosDbServiceProduct.GetProductAsync(lastSimulationDetails.product).Result;

                Results result = new Results();

                //Machine 1

                result.totalConstructedProductsMachine1 = (int)(machineOne.amountProductManufacturedPerHour * realHoursProduction);
                result.grossTotalProfitMachine1 = result.totalConstructedProductsMachine1 * product.price;
                result.realSimulationProfitMachine1 = (result.grossTotalProfitMachine1 - (lastSimulationDetails.productManufacturePrice * result.totalConstructedProductsMachine1) - (machineOne.costPerHour * realHoursProduction));

                //Machine 2

                result.totalConstructedProductsMachine2 = (int)(machineTwo.amountProductManufacturedPerHour * realHoursProduction);
                result.grossTotalProfitMachine2 = result.totalConstructedProductsMachine2 * product.price;
                result.realSimulationProfitMachine2 = (result.grossTotalProfitMachine2 - (lastSimulationDetails.productManufacturePrice * result.totalConstructedProductsMachine2) - (machineTwo.costPerHour * realHoursProduction));

                this._cosmosDbServiceResults.AddResultAsync(result);

                #endregion

                return View();
            }

            catch (Exception)
            {
                return RedirectToAction("SimulationExecutionError");
            }
        }
    }
}
