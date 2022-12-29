using Microsoft.AspNetCore.Mvc;
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

        public async Task<IActionResult> Index()
        {
            return View(await _departmentRepository.GetAllAsync());
        }

        public async Task<IActionResult> Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Department department)
        {
            await _departmentRepository.CreateAsync(department);
            return RedirectToAction("Index");
        }
    }
}
