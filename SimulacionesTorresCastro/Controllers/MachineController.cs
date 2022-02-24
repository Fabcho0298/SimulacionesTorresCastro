using Microsoft.AspNetCore.Mvc;
using SimulacionesTorresCastro.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimulacionesTorresCastro.Controllers
{
    public class MachineController : Controller
    {
        private readonly ICosmosDBServiceMachine _cosmosDbService;

        public MachineController(ICosmosDBServiceMachine cosmosDBService)
        {
            this._cosmosDbService = cosmosDBService;
        }

        public async Task<ActionResult> Machines()
        {
            return View((await _cosmosDbService.GetMachinesAsync("SELECT * FROM machine")).ToList());
        }

        public async Task<ActionResult> CreateMachine(Machine machine)
        {
            machine.id = Guid.NewGuid().ToString();
            await this._cosmosDbService.AddMachineAsync(machine);
            return RedirectToAction("Machines");
        }

        public IActionResult Create()
        {
            return View();
        }

        public async Task<ActionResult> EditMachine(Machine machine)
        {
            await this._cosmosDbService.UpdateMachineAsync(machine.id, machine);
            return RedirectToAction("Machines");
        }
        public IActionResult Edit(Machine machine)
        {
            return View(machine);
        }

        public async Task<ActionResult> DeleteMachine(Machine machine)
        {
            await _cosmosDbService.DeleteMachineAsync(machine.id);
            return RedirectToAction("Machines");
        }

        public ActionResult Delete(Machine machine)
        {
            return View(machine);
        }
    }
}
