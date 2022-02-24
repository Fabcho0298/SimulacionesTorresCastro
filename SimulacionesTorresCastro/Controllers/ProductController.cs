using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SimulacionesTorresCastro.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimulacionesTorresCastro.Controllers
{
    public class ProductController : Controller
    {
        private readonly ICosmosDBServiceProduct _cosmosDbService;

        public ProductController(ICosmosDBServiceProduct cosmosDBService)
        {
            this._cosmosDbService = cosmosDBService;
        }

        public async Task<ActionResult> Products()
        {
            return View((await _cosmosDbService.GetProductsAsync("SELECT * FROM product")).ToList());
        }

        public async Task<ActionResult> CreateProduct(Product product)
        {
            product.id = Guid.NewGuid().ToString();
            await this._cosmosDbService.AddProductAsync(product);
            return RedirectToAction("Products");
        }

        public IActionResult Create()
        {
            return View();
        }

        public async Task<ActionResult> EditProduct(Product product)
        {
            await this._cosmosDbService.UpdateProductAsync(product.id, product);
            return RedirectToAction("Products");
        }
        public IActionResult Edit(Product product)
        {
            return View(product);
        }

        public async Task<ActionResult> DeleteProduct(Product product)
        {
            await _cosmosDbService.DeleteProductAsync(product.id);
            return RedirectToAction("Products");
        }

        public ActionResult Delete(Product product)
        {
            return View(product);
        }

    }
}
