using SupermarketApp.Models;

namespace SupermarketApp.Service
{
    public interface IDepartmentService
    {
        Task<IEnumerable<Department>> GetDepartmentsAsync();

    }
}
