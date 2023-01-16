using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SupermarketApp.Core.Service.Interfaces;
using SupermarketApp.Core.Models;

namespace SupermarketApp.Core.Controllers
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
            var manufacturers = await _manService.GetManufacturersAsync();
            return View(manufacturers);
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
        public async Task<IActionResult> Create(ManufacturerModel manufacturer)
        {
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
        public async Task<IActionResult> Edit(int id, ManufacturerModel manufacturer)
        {
            if (id != manufacturer.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _manService.UpdateManufacturerAsync(manufacturer);
                    return RedirectToAction(nameof(Index));
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
            }

            return View(manufacturer);
        }

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

        private async Task<string> ImageToStringAsync(IFormFile imageFile)
        {
            using var ms = new MemoryStream();
            await imageFile.CopyToAsync(ms);

            return Convert.ToBase64String(ms.ToArray());
        }

        private bool ManufacturerExists(int id)
        {
            return _manService.FindManufacturerByIdAsync(id).Result is not null;
        }
    }
}
