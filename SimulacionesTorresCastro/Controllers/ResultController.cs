using Microsoft.AspNetCore.Mvc;
using SimulacionesTorresCastro.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimulacionesTorresCastro.Controllers
{
    public class ResultController : Controller
    {

        private readonly ICosmosDBServiceResults _cosmosDbService;

        public ResultController(ICosmosDBServiceResults cosmosDBService)
        {
            this._cosmosDbService = cosmosDBService;
        }

        public async Task<ActionResult> Results()
        {
            return View((await _cosmosDbService.GetResultsAsync("SELECT * FROM results")).ToList());
        }

        public async Task<ActionResult> CreateResults(Results results)
        {
            results.id = Guid.NewGuid().ToString();
            await this._cosmosDbService.AddResultAsync(results);
            return RedirectToAction("Index");
        }

        public IActionResult Create()
        {
            return View();
        }
        public IActionResult Index()
        {
            return View();
        }
    }
}
