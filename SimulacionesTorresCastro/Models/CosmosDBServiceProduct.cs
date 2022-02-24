using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Azure.Cosmos;

namespace SimulacionesTorresCastro.Models
{

    public interface ICosmosDBServiceProduct
    {
        Task<IEnumerable<Product>> GetProductsAsync(string query);
        Task<Product> GetProductAsync(string id);
        Task AddProductAsync(Product product);
        Task DeleteProductAsync(string id);
        Task UpdateProductAsync(string id, Product product);
    }

    public class CosmosDBServiceProduct
    {
        private Container _container;

        public CosmosDBServiceProduct(CosmosClient client, string databaseName, string containerName)
        {
            this._container = client.GetContainer(databaseName, containerName);
        }

        public async Task<IEnumerable<Product>> GetProductsAsync(string queryString)
        {
            var query = this._container.GetItemQueryIterator<Product>(new QueryDefinition(queryString));
            List<Product> results = new List<Product>();
            while (query.HasMoreResults)
            {
                var response = await query.ReadNextAsync();
                results.AddRange(response.ToList());
            }
            return results;
        }


        public async Task<Product> GetProductAsync(string id)
        {
            try
            {
                ItemResponse<Product> response = await this._container.ReadItemAsync<Product>(id, new PartitionKey(id));
                return response.Resource;
            }
            catch (CosmosException ex) when (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                return null;
            }
        }

        public async Task AddProductAsync(Product product)
        {
            await this._container.CreateItemAsync<Product>(product, new PartitionKey(product.id));
        }

        public async Task DeleteProductAsync(string id)
        {
            await this._container.DeleteItemAsync<Product>(id, new PartitionKey(id));
        }

        public async Task UpdateProductoAsync(string id, Product product)
        {
            await this._container.UpsertItemAsync<Product>(product, new PartitionKey(id));
        }

    }
}
