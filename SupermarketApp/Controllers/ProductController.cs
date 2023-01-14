using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SupermarketApp.BL.Service.Interfaces;
using SupermarketApp.Data.Models;

namespace SupermarketApp.Core.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductService _prodService;
        public ProductController(IProductService service)
        {
            _prodService = service;
        }

        public async Task<IActionResult> Index()
        {
            var products = await _prodService.GetProductsAsync();

            return View(products);
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id is null)
            {
                return NotFound();
            }

            var product = await _prodService.FindProductByIdAsync(id.Value);

            if (product is null)
            {
                return NotFound();
            }

            return View(product);
        }

        public async Task<IActionResult> Create(int? id)
        {
            var product = new ProductModel();

            if (id is not null)
            {
                product.ManufacturerId = id.Value;
            }

            return View(product);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateProduct(ProductModel product)
        {
            if (ModelState.IsValid)
            {
                await _prodService.CreateProductAsync(product);
                return RedirectToAction(nameof(Index));
            }

            return View(product);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id is null)
            {
                return NotFound();
            }

            var product = await _prodService.FindProductByIdAsync(id.Value);

            if (product is null)
            {
                return NotFound();
            }

            return View(product);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, ProductModel product)
        {
            if (id != product.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _prodService.UpdateProductAsync(product);
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductExists(product.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
            }

            return View(product);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id is null)
            {
                return NotFound();
            }

            var product = await _prodService.FindProductByIdAsync(id.Value);

            if (product is null)
            {
                return NotFound();
            }

            return View(product);
        }

        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var product = await _prodService.FindProductByIdAsync(id);

            if (product is not null)
            {
                await _prodService.RemoveProductAsync(product);
            }

            return RedirectToAction(nameof(Index));
        }

        private bool ProductExists(int id)
        {
            return _prodService.FindProductByIdAsync(id).Result is not null;
        }
    }
}
