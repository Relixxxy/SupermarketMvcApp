using FluentValidation;
using FluentValidation.AspNetCore;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SupermarketApp.Data.Entities;
using SupermarketApp.BL.Service.Interfaces;

namespace SupermarketApp.Core.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductService _prodService;
        private readonly IValidator<Product> _validator;
        public ProductController(IProductService service, IValidator<Product> validator)
        {
            _prodService = service;
            _validator = validator;
        }

        public async Task<IActionResult> Index()
        {
            var products = await _prodService.GetProductsWithIncludeAsync(e => e.Department, e => e.Manufacturer);

            return View(products);
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id is null)
            {
                return NotFound();
            }

            var product = await _prodService.FindProductByIdWithIncludeAsync(id.Value, p => p.Department, p => p.Manufacturer);

            if (product is null)
            {
                return NotFound();
            }

            return View(product);
        }

        public async Task<IActionResult> Create()
        {
            ViewData["DepartmentId"] = new SelectList(await _prodService.GetDepartmentsAsync(), "Id", "Name");
            ViewData["ManufacturerId"] = new SelectList(await _prodService.GetManufacturersAsync(), "Id", "Name");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Product product, IFormFile imageFile)
        {
            if (imageFile is not null)
            {
                product.Image = await ImageToStringAsync(imageFile);
                ModelState[nameof(product.Image)].ValidationState = ModelValidationState.Valid;
            }

            if (ModelState.IsValid)
            {
                ValidationResult result = _validator.Validate(product);

                if (result.IsValid)
                {
                    await _prodService.CreateProductAsync(product);
                    return RedirectToAction(nameof(Index));
                }

                result.AddToModelState(ModelState);
            }

            ViewData["DepartmentId"] = new SelectList(await _prodService.GetDepartmentsAsync(), "Id", "Name", product.DepartmentId);
            ViewData["ManufacturerId"] = new SelectList(await _prodService.GetManufacturersAsync(), "Id", "Name", product.ManufacturerId);
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

            ViewData["DepartmentId"] = new SelectList(await _prodService.GetDepartmentsAsync(), "Id", "Name", product.DepartmentId);
            ViewData["ManufacturerId"] = new SelectList(await _prodService.GetManufacturersAsync(), "Id", "Name", product.ManufacturerId);
            return View(product);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Product product, IFormFile? imageFile)
        {
            if (id != product.Id)
            {
                return NotFound();
            }

            if (imageFile is not null)
            {
                product.Image = await ImageToStringAsync(imageFile);
            }

            if (ModelState.IsValid)
            {
                try
                {
                    ValidationResult result = _validator.Validate(product);

                    if (result.IsValid)
                    {
                        await _prodService.UpdateProductAsync(product);
                        return RedirectToAction(nameof(Index));
                    }

                    result.AddToModelState(ModelState);
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

            ViewData["DepartmentId"] = new SelectList(await _prodService.GetDepartmentsAsync(), "Id", "Name", product.DepartmentId);
            ViewData["ManufacturerId"] = new SelectList(await _prodService.GetManufacturersAsync(), "Id", "Name", product.ManufacturerId);
            return View(product);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id is null)
            {
                return NotFound();
            }

            var product = await _prodService.FindProductByIdWithIncludeAsync(id.Value, p => p.Department, p => p.Manufacturer);

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

        private async Task<string> ImageToStringAsync(IFormFile imageFile)
        {
            using var ms = new MemoryStream();
            await imageFile.CopyToAsync(ms);

            return Convert.ToBase64String(ms.ToArray());
        }

        private bool ProductExists(int id)
        {
            return _prodService.FindProductByIdAsync(id).Result is not null;
        }
    }
}
