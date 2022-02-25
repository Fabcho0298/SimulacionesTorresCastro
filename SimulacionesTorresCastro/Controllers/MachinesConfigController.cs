using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimulacionesTorresCastro.Controllers
{
    public class MachinesConfigController : Controller
    {
        public IActionResult MachinesConfig()
        {
            return View();
        }

        public IActionResult ButtonPress(string button)
        {
            if (!string.IsNullOrEmpty(button))
            {
                if (button.Equals("Machines"))
                {
                    return RedirectToAction("Machines", "Machine");
                }

                else
                {
                    return RedirectToAction("Products", "Product");
                }
            }

            else
            {

                return RedirectToAction("MachinesConfig");
            }

        }
    }
}
