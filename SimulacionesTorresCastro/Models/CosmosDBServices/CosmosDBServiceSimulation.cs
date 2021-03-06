using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Azure.Cosmos;

namespace SimulacionesTorresCastro.Models
{

    public interface ICosmosDBServiceSimulationDetails
    {
        Task<IEnumerable<SimulationDetails>> GetSimulationsDetailsAsync(string query);
        Task<SimulationDetails> GetSimulationDetailsAsync(string id);
        Task AddSimulationDetailsAsync(SimulationDetails simulation);
        Task UpdateSimulationDetailsAsync(string id, SimulationDetails simulation);
        Task DeleteSimulationDetailsAsync(string id);


    }

    public class CosmosDBServiceSimulationDetails : ICosmosDBServiceSimulationDetails
    {
        private Container _container;

        public CosmosDBServiceSimulationDetails(CosmosClient client, string databaseName, string containerName)
        {
            this._container = client.GetContainer(databaseName, containerName);
        }

        public async Task<IEnumerable<SimulationDetails>> GetSimulationsDetailsAsync(string queryString)
        {
            var query = this._container.GetItemQueryIterator<SimulationDetails>(new QueryDefinition(queryString));
            List<SimulationDetails> results = new List<SimulationDetails>();
            while (query.HasMoreResults)
            {
                var response = await query.ReadNextAsync();
                results.AddRange(response.ToList());
            }
            return results;
        }


        public async Task<SimulationDetails> GetSimulationDetailsAsync(string id)
        {
            try
            {
                ItemResponse<SimulationDetails> response = await this._container.ReadItemAsync<SimulationDetails>(id, new PartitionKey(id));
                return response.Resource;
            }
            catch (CosmosException ex) when (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                return null;
            }
        }

        public async Task AddSimulationDetailsAsync(SimulationDetails simulation)
        {
            await this._container.CreateItemAsync<SimulationDetails>(simulation, new PartitionKey(simulation.id));
        }

        public async Task UpdateSimulationDetailsAsync(string id, SimulationDetails simulation)
        {
            await this._container.UpsertItemAsync<SimulationDetails>(simulation, new PartitionKey(id));
        }

        public async Task DeleteSimulationDetailsAsync(string id)
        {
            await this._container.DeleteItemAsync<SimulationDetails>(id, new PartitionKey(id));
        }
    }
}
