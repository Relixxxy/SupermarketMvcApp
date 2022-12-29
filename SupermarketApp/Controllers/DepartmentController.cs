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
            _departmentRepository.GetAllAsync();
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}
