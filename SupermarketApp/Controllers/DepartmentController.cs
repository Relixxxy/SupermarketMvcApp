using System.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using SupermarketApp.Data.Repository;
using SupermarketApp.Models;

namespace SupermarketApp.Controllers
{
    public class DepartmentController : Controller
    {
        private readonly IRepository<Department> _departmentRepository;
        public DepartmentController(IRepository<Department> departmentRep)
        {
            _departmentRepository = departmentRep;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            return View(await _departmentRepository.GetAllAsync());
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Department department, IFormFile image)
        {
            if (image is not null)
            {
                department.Image = await ImageToStringAsync(image);
                ModelState[nameof(department.Image)].ValidationState = ModelValidationState.Valid;
            }

            try
            {
                if (ModelState.IsValid)
                {
                    await _departmentRepository.CreateAsync(department);
                    return RedirectToAction(nameof(Index));
                }
            }
            catch (DataException ex)
            {
            }

            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id is null)
            {
                return NotFound();
            }

            var department = await _departmentRepository.FindByIdAsync(id.Value);

            if (department is null)
            {
                return NotFound();
            }

            return View(department);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int? id, Department department, IFormFile image)
        {
            if (id != department.Id)
            {
                return NotFound();
            }

            if (image is not null)
            {
                department.Image = await ImageToStringAsync(image);
                ModelState[nameof(department.Image)].ValidationState = ModelValidationState.Valid;
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _departmentRepository.UpdateAsync(department);
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

                return RedirectToAction(nameof(Index));
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

            var department = await _departmentRepository.FindByIdAsync(id.Value);

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

            var department = await _departmentRepository.FindByIdAsync(id.Value);

            if (department is null)
            {
                return NotFound();
            }

            return View(department);
        }

        // POST: Manufacturer/Delete/5
        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var department = await _departmentRepository.FindByIdAsync(id);

            if (department != null)
            {
                await _departmentRepository.RemoveAsync(department);
            }

            return RedirectToAction(nameof(Index));
        }

        private async Task<string> ImageToStringAsync(IFormFile image)
        {
            using var ms = new MemoryStream();
            await image.CopyToAsync(ms);

            return Convert.ToBase64String(ms.ToArray());
        }

        private bool DepartmentExists(int id)
        {
            return _departmentRepository.FindByIdAsync(id) is not null;
        }
    }
}
