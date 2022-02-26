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

        public IActionResult ResultsError()
        {
            return View();
        }

        public async Task<ActionResult> Results()
        {
            try
            {
                return View((await _cosmosDbService.GetResultsAsync("SELECT * FROM results")).ToList().Last());
            }
            catch (Exception)
            {
                return RedirectToAction("ResultsError");
            }
        }

        public async Task<ActionResult> CreateResults(Results results)
        {
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
