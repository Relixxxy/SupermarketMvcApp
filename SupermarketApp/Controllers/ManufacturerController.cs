using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using SupermarketApp.Models;
using SupermarketApp.Service.Interfaces;

namespace SupermarketApp.Controllers
{
    public class ManufacturerController : Controller
    {
        private readonly IManufacturerService _manService;

        public ManufacturerController(IManufacturerService service)
        {
            _manService = service;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _manService.GetManufacturersAsync());
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id is null)
            {
                return NotFound();
            }

            var manufacturer = await _manService.FindManufacturerByIdAsync(id.Value);

            if (manufacturer is null)
            {
                return NotFound();
            }

            return View(manufacturer);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Manufacturer manufacturer, IFormFile image)
        {
            if (image is not null)
            {
                manufacturer.Image = await ImageToStringAsync(image);
                ModelState[nameof(manufacturer.Image)].ValidationState = ModelValidationState.Valid;
            }

            if (ModelState.IsValid)
            {
                await _manService.CreateManufacturerAsync(manufacturer);
                return RedirectToAction(nameof(Index));
            }

            return View(manufacturer);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id is null)
            {
                return NotFound();
            }

            var manufacturer = await _manService.FindManufacturerByIdAsync(id.Value);

            if (manufacturer is null)
            {
                return NotFound();
            }

            return View(manufacturer);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Manufacturer manufacturer, IFormFile image)
        {
            if (id != manufacturer.Id)
            {
                return NotFound();
            }

            if (image is not null)
            {
                manufacturer.Image = await ImageToStringAsync(image);
                ModelState[nameof(manufacturer.Image)].ValidationState = ModelValidationState.Valid;
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _manService.UpdateManufacturerAsync(manufacturer);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ManufacturerExists(manufacturer.Id))
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

            return View(manufacturer);
        }

        // GET: Manufacturer/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id is null)
            {
                return NotFound();
            }

            var manufacturer = await _manService.FindManufacturerByIdAsync(id.Value);

            if (manufacturer is null)
            {
                return NotFound();
            }

            return View(manufacturer);
        }

        // POST: Manufacturer/Delete/5
        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var manufacturer = await _manService.FindManufacturerByIdAsync(id);

            if (manufacturer is null)
            {
                return NotFound();
            }

            await _manService.RemoveManufacturerAsync(manufacturer);

            return RedirectToAction(nameof(Index));
        }

        private async Task<string> ImageToStringAsync(IFormFile image)
        {
            using var ms = new MemoryStream();
            await image.CopyToAsync(ms);

            return Convert.ToBase64String(ms.ToArray());
        }

        private bool ManufacturerExists(int id)
        {
            return _manService.FindManufacturerByIdAsync(id).Result is not null;
        }
    }
}
