using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimulacionesTorresCastro.Models
{
    public class Simulation
    {
        public SimulationDetails simulationDetails { get; set; }
        public IEnumerable<Product> simulationProducts { get; set; }
        public IEnumerable<Machine> simulationMachines { get; set; }
    }
}
