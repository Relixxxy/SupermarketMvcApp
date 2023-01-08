using FluentValidation;
using FluentValidation.AspNetCore;
using FluentValidation.Results;
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
        private readonly IValidator<Manufacturer> _validator;

        public ManufacturerController(IManufacturerService service, IValidator<Manufacturer> validator)
        {
            _manService = service;
            _validator = validator;
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
        public async Task<IActionResult> Create(Manufacturer manufacturer, IFormFile imageFile)
        {
            if (imageFile is not null)
            {
                manufacturer.Image = await ImageToStringAsync(imageFile);
                ModelState[nameof(manufacturer.Image)].ValidationState = ModelValidationState.Valid;
            }

            if (ModelState.IsValid)
            {
                ValidationResult result = _validator.Validate(manufacturer);

                if (result.IsValid)
                {
                    await _manService.CreateManufacturerAsync(manufacturer);
                    return RedirectToAction(nameof(Index));
                }

                result.AddToModelState(ModelState);
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
        public async Task<IActionResult> Edit(int id, Manufacturer manufacturer, IFormFile? imageFile)
        {
            if (id != manufacturer.Id)
            {
                return NotFound();
            }

            if (imageFile is not null)
            {
                manufacturer.Image = await ImageToStringAsync(imageFile);
            }

            if (ModelState.IsValid)
            {
                try
                {
                    ValidationResult result = _validator.Validate(manufacturer);

                    if (result.IsValid)
                    {
                        await _manService.UpdateManufacturerAsync(manufacturer);
                        return RedirectToAction(nameof(Index));
                    }

                    result.AddToModelState(ModelState);
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
