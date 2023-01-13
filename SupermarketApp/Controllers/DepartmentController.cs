using System.Data;
using FluentValidation;
using FluentValidation.AspNetCore;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using SupermarketApp.Data.Entities;
using SupermarketApp.BL.Service.Interfaces;
using SupermarketApp.Data.Models;

namespace SupermarketApp.Core.Controllers
{
    public class DepartmentController : Controller
    {
        private readonly IDepartmentService _departService;

        public DepartmentController(IDepartmentService departService)
        {
            _departService = departService;
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
        public async Task<IActionResult> Create(DepartmentModel department)
        {
            if (ModelState.IsValid)
            {
                await _departService.CreateDepartmentAsync(department);
                return RedirectToAction(nameof(Index));
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
        public async Task<ActionResult> Edit(int? id, DepartmentModel department)
        {
            if (id != department.Id)
            {
                NotFound();
            }

            try
            {
                if (ModelState.IsValid)
                {
                    await _departService.UpdateDepartmentAsync(department);
                    return RedirectToAction(nameof(Index));
                }
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

        private bool DepartmentExists(int id)
        {
            return _departService.FindDepartmentByIdAsync(id).Result is not null;
        }
    }
}
