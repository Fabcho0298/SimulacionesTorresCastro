using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Azure.Cosmos;


namespace SimulacionesTorresCastro.Models
{


    public interface ICosmosDBServiceResults
    {
        Task<IEnumerable<Results>> GetResultsAsync(string query);
        Task<Results> GetResultAsync(string id);
        Task AddResultAsync(Results result);

    }

    public class CosmosDBServiceResults : ICosmosDBServiceResults
    {
        private Container _container;

        public CosmosDBServiceResults(CosmosClient client, string databaseName, string containerName)
        {
            this._container = client.GetContainer(databaseName, containerName);
        }

        public async Task<IEnumerable<Results>> GetResultsAsync(string queryString)
        {
            var query = this._container.GetItemQueryIterator<Results>(new QueryDefinition(queryString));
            List<Results> results = new List<Results>();
            while (query.HasMoreResults)
            {
                var response = await query.ReadNextAsync();
                results.AddRange(response.ToList());
            }
            return results;
        }

        public async Task<Results> GetResultAsync(string id)
        {
            try
            {
                ItemResponse<Results> response = await this._container.ReadItemAsync<Results>(id, new PartitionKey(id));
                return response.Resource;
            }
            catch (CosmosException ex) when (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                return null;
            }
        }

        public async Task AddResultAsync(Results result)
        {
            await this._container.CreateItemAsync<Results>(result, new PartitionKey(result.id));
        }
    }
}
