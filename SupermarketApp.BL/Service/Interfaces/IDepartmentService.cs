using SupermarketApp.Data.Models;

namespace SupermarketApp.BL.Service.Interfaces
{
    public interface IDepartmentService
    {
        Task CreateDepartmentAsync(DepartmentModel departmentModel);
        Task<DepartmentModel> FindDepartmentByIdAsync(int id);
        Task<IEnumerable<DepartmentModel>> GetDepartmentsAsync();
        Task RemoveDepartmentAsync(DepartmentModel departmentModel);
        Task UpdateDepartmentAsync(DepartmentModel departmentModel);
        Task<IEnumerable<ProductModel>> GetProductsAsync();
    }
}
