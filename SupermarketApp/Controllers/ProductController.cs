using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SupermarketApp.Data.Context;
using SupermarketApp.Data.Repository;
using SupermarketApp.Models;

namespace SupermarketApp.Controllers
{
    public class ProductController : Controller
    {
        private readonly IRepository<Product> _productResository;
        private readonly IRepository<Department> _departmentResository;
        private readonly IRepository<Manufacturer> _manufacturerResository;

        public ProductController(IRepository<Product> pr, IRepository<Department> dr, IRepository<Manufacturer> mr)
        {
            _productResository = pr;
            _departmentResository = dr;
            _manufacturerResository = mr;
        }

        // GET: Product
        public async Task<IActionResult> Index()
        {
            var products = await _productResository.GetAllWithIncludeAsync(e => e.Department, e => e.Manufacturer);

            return View(products);
        }

        // GET: Product/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id is null)
            {
                return NotFound();
            }

            var product = await _productResository.FindByIdWithIncludeAsync(id.Value, p => p.Department, p => p.Manufacturer);

            if (product is null)
            {
                return NotFound();
            }

            return View(product);
        }

        // GET: Product/Create
        public async Task<IActionResult> Create()
        {
            ViewData["DepartmentId"] = new SelectList(await _departmentResository.GetAllAsync(), "Id", "Name");
            ViewData["ManufacturerId"] = new SelectList(await _manufacturerResository.GetAllAsync(), "Id", "Name");
            return View();
        }

        // POST: Product/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Product product, IFormFile image)
        {
            if (image is not null)
            {
                product.Image = await ImageToStringAsync(image);
                ModelState[nameof(product.Image)].ValidationState = ModelValidationState.Valid;
            }

            if (ModelState.IsValid)
            {
                await _productResository.CreateAsync(product);
                return RedirectToAction(nameof(Index));
            }

            ViewData["DepartmentId"] = new SelectList(await _departmentResository.GetAllAsync(), "Id", "Name", product.DepartmentId);
            ViewData["ManufacturerId"] = new SelectList(await _manufacturerResository.GetAllAsync(), "Id", "Name", product.ManufacturerId);
            return View(product);
        }

        // GET: Product/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id is null)
            {
                return NotFound();
            }

            var product = await _productResository.FindByIdAsync(id.Value);

            if (product is null)
            {
                return NotFound();
            }

            ViewData["DepartmentId"] = new SelectList(await _departmentResository.GetAllAsync(), "Id", "Name", product.DepartmentId);
            ViewData["ManufacturerId"] = new SelectList(await _manufacturerResository.GetAllAsync(), "Id", "Name", product.ManufacturerId);
            return View(product);
        }

        // POST: Product/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Product product, IFormFile image)
        {
            if (id != product.Id)
            {
                return NotFound();
            }

            if (image is not null)
            {
                product.Image = await ImageToStringAsync(image);
                ModelState[nameof(product.Image)].ValidationState = ModelValidationState.Valid;
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _productResository.UpdateAsync(product);
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

                return RedirectToAction(nameof(Index));
            }

            ViewData["DepartmentId"] = new SelectList(await _departmentResository.GetAllAsync(), "Id", "Name", product.DepartmentId);
            ViewData["ManufacturerId"] = new SelectList(await _manufacturerResository.GetAllAsync(), "Id", "Name", product.ManufacturerId);
            return View(product);
        }

        // GET: Product/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id is null)
            {
                return NotFound();
            }

            var product = await _productResository.FindByIdWithIncludeAsync(id.Value, p => p.Department, p => p.Manufacturer);

            if (product is null)
            {
                return NotFound();
            }

            return View(product);
        }

        // POST: Product/Delete/5
        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var product = await _productResository.FindByIdAsync(id);

            if (product is not null)
            {
                await _productResository.RemoveAsync(product);
            }

            return RedirectToAction(nameof(Index));
        }
        private async Task<string> ImageToStringAsync(IFormFile image)
        {
            using var ms = new MemoryStream();
            await image.CopyToAsync(ms);

            return Convert.ToBase64String(ms.ToArray());
        }

        private bool ProductExists(int id)
        {
            return _productResository.FindByIdAsync(id) is not null;
        }
    }
}
