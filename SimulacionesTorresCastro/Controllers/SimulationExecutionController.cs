using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimulacionesTorresCastro.Controllers
{
    public class SimulationExecutionController : Controller
    {
        public IActionResult SimulationExecution()
        {
            return View();
        }
    }
}
