using Microsoft.Azure.Cosmos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimulacionesTorresCastro
{
    public class CosmosDBService
    {
        private CosmosClient client;
        private string databaseName;
        private string containerName;

        public CosmosDBService(CosmosClient client, string databaseName, string containerName)
        {
            this.client = client;
            this.databaseName = databaseName;
            this.containerName = containerName;
        }
    }
}
