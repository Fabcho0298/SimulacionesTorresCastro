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
            try
            {
                return View((await _cosmosDbService.GetProductsAsync("SELECT * FROM product")).ToList());
            }
            catch (Exception)
            {
                return RedirectToAction("ProductError");
            }
        }

        public async Task<ActionResult> CreateProduct(Product product)
        {
            try
            {
                product.id = Guid.NewGuid().ToString();
                await this._cosmosDbService.AddProductAsync(product);
                return RedirectToAction("Products");

            }
            catch (Exception)
            {
                return RedirectToAction("ProductError");
            }

        }

        public IActionResult Create()
        {
            try
            {
                return View();

            }
            catch (Exception)
            {
                return RedirectToAction("ProductError");
            }
            
        }

        public async Task<ActionResult> EditProduct(Product product)
        {
            try
            {
                await this._cosmosDbService.UpdateProductAsync(product.id, product);
                return RedirectToAction("Products");

            }
            catch (Exception)
            {
                return RedirectToAction("ProductError");
            }

        }
        public IActionResult Edit(Product product)
        {
            try
            {
                return View(product);

            }
            catch (Exception)
            {
                return RedirectToAction("ProductError");
            }
        }

        public async Task<ActionResult> DeleteProduct(Product product)
        {
            try
            {
                await _cosmosDbService.DeleteProductAsync(product.id);
                return RedirectToAction("Products");
            }
            catch (Exception)
            {
                return RedirectToAction("ProductError");
            }

        }

        public ActionResult Delete(Product product)
        {

            try
            {
                return View(product);
            }
            catch (Exception)
            {
                return RedirectToAction("ProductError");
            }
        }


        public IActionResult ProductError()
        {
            return View();
        }

    }
}
