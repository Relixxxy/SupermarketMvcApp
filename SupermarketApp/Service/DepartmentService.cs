using SupermarketApp.Data.Repository;
using SupermarketApp.Models;
using SupermarketApp.Service.Interfaces;

namespace SupermarketApp.Service
{
    public class DepartmentService : IDepartmentService
    {
        private readonly IRepository<Department> _repository;

        public DepartmentService(IRepository<Department> repository)
        {
            _repository = repository;
        }

        public async Task CreateDepartmentAsync(Department department)
        {
            await _repository.CreateAsync(department);
        }

        public async Task<Department> FindDepartmentByIdAsync(int id)
        {
            return await _repository.FindByIdAsync(id);
        }

        public async Task<IEnumerable<Department>> GetDepartmentsAsync()
        {
            return await _repository.GetAllAsync();
        }

        public async Task RemoveDepartmentAsync(Department department)
        {
            await _repository.RemoveAsync(department);
        }

        public async Task UpdateDepartmentAsync(Department department)
        {
            await _repository.UpdateAsync(department);
        }
    }
}
