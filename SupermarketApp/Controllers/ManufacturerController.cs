using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using SupermarketApp.Data.Repository;
using SupermarketApp.Models;

namespace SupermarketApp.Controllers
{
    public class ManufacturerController : Controller
    {
        private readonly IRepository<Manufacturer> _manufacturerRepository;

        public ManufacturerController(IRepository<Manufacturer> repository)
        {
            _manufacturerRepository = repository;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _manufacturerRepository.GetAllAsync());
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id is null)
            {
                return NotFound();
            }

            var manufacturer = await _manufacturerRepository.FindByIdAsync(id.Value);

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
                await _manufacturerRepository.CreateAsync(manufacturer);
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

            var manufacturer = await _manufacturerRepository.FindByIdAsync(id.Value);

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
                    await _manufacturerRepository.UpdateAsync(manufacturer);
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

            var manufacturer = await _manufacturerRepository.FindByIdAsync(id.Value);

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
            var manufacturer = await _manufacturerRepository.FindByIdAsync(id);

            if (manufacturer is null)
            {
                return NotFound();
            }

            await _manufacturerRepository.RemoveAsync(manufacturer);

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
            return _manufacturerRepository.FindByIdAsync(id) is not null;
        }
    }
}
