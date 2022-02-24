using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Azure.Cosmos;


namespace SimulacionesTorresCastro.Models
{

    public interface ICosmosDBServiceMachine
    {
        Task<IEnumerable<Machine>> GetMachinesAsync(string query);
        Task<Machine> GetMachineAsync(string id);
        Task AddMachineAsync(Machine machine);
        Task DeleteMachineAsync(string id);
        Task UpdateMachineAsync(string id, Machine machine);
    }

    public class CosmosDBServiceMachine
    {
        private Container _container;

        public CosmosDBServiceMachine(CosmosClient client, string databaseName, string containerName)
        {
            this._container = client.GetContainer(databaseName, containerName);
        }

        public async Task<IEnumerable<Machine>> GetMachinesAsync(string queryString)
        {
            var query = this._container.GetItemQueryIterator<Machine>(new QueryDefinition(queryString));
            List<Machine> results = new List<Machine>();
            while (query.HasMoreResults)
            {
                var response = await query.ReadNextAsync();
                results.AddRange(response.ToList());
            }
            return results;
        }

        public async Task<Machine> GetMachineAsync(string id)
        {
            try
            {
                ItemResponse<Machine> response = await this._container.ReadItemAsync<Machine>(id, new PartitionKey(id));
                return response.Resource;
            }
            catch (CosmosException ex) when (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                return null;
            }
        }

        public async Task AddMachineAsync(Machine machine)
        {
            await this._container.CreateItemAsync<Machine>(machine, new PartitionKey(machine.id));
        }

        public async Task DeleteMachineAsync(string id)
        {
            await this._container.DeleteItemAsync<Machine>(id, new PartitionKey(id));
        }

        public async Task UpdateMachineAsync(string id, Machine machine)
        {
            await this._container.UpsertItemAsync<Machine>(machine, new PartitionKey(id));
        }
    }
}
