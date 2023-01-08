using System.Data;
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
    public class DepartmentController : Controller
    {
        private readonly IDepartmentService _departService;
        private readonly IValidator<Department> _validator;

        public DepartmentController(IDepartmentService departService, IValidator<Department> validator)
        {
            _departService = departService;
            _validator = validator;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            return View(await _departService.GetDepartmentsAsync());
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Department department, IFormFile imageFile)
        {
            if (imageFile is not null)
            {
                department.Image = await ImageToStringAsync(imageFile);
                ModelState[nameof(department.Image)].ValidationState = ModelValidationState.Valid;
            }

            try
            {
                if (ModelState.IsValid)
                {
                    ValidationResult result = await _validator.ValidateAsync(department);

                    if (result.IsValid)
                    {
                        await _departService.CreateDepartmentAsync(department);
                        return RedirectToAction(nameof(Index));
                    }

                    result.AddToModelState(ModelState);
                }
            }
            catch (DataException ex)
            {
            }

            return View(department);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id is null)
            {
                return NotFound();
            }

            var department = await _departService.FindDepartmentByIdAsync(id.Value);

            if (department is null)
            {
                return NotFound();
            }

            return View(department);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int? id, Department department, IFormFile? imageFile)
        {
            if (id != department.Id)
            {
                return NotFound();
            }

            if (imageFile is not null)
            {
                department.Image = await ImageToStringAsync(imageFile);
            }

            if (ModelState.IsValid)
            {
                try
                {
                    ValidationResult result = await _validator.ValidateAsync(department);
                    if (result.IsValid)
                    {
                        await _departService.UpdateDepartmentAsync(department);
                        return RedirectToAction(nameof(Index));
                    }

                    result.AddToModelState(ModelState);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DepartmentExists(department.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
            }

            return View(department);
        }

        [HttpGet]
        public async Task<IActionResult> Details(int? id)
        {
            if (id is null)
            {
                return NotFound();
            }

            var department = await _departService.FindDepartmentByIdAsync(id.Value);

            if (department is null)
            {
                return NotFound();
            }

            return View(department);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id is null)
            {
                return NotFound();
            }

            var department = await _departService.FindDepartmentByIdAsync(id.Value);

            if (department is null)
            {
                return NotFound();
            }

            return View(department);
        }

        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var department = await _departService.FindDepartmentByIdAsync(id);

            if (department is not null)
            {
                await _departService.RemoveDepartmentAsync(department);
            }

            return RedirectToAction(nameof(Index));
        }

        private async Task<string> ImageToStringAsync(IFormFile imageFile)
        {
            using var ms = new MemoryStream();
            await imageFile.CopyToAsync(ms);

            return Convert.ToBase64String(ms.ToArray());
        }

        private bool DepartmentExists(int id)
        {
            return _departService.FindDepartmentByIdAsync(id).Result is not null;
        }
    }
}
