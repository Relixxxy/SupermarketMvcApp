using SupermarketApp.Models;

namespace SupermarketApp.Service.Interfaces
{
    public interface IDepartmentService
    {
        Task CreateDepartmentAsync(Department department);
        Task<Department> FindDepartmentByIdAsync(int id);
        Task<IEnumerable<Department>> GetDepartmentsAsync();
        Task RemoveDepartmentAsync(Department department);
        Task UpdateDepartmentAsync(Department department);
    }
}
