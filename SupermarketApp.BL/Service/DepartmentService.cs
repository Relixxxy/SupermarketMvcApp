using SupermarketApp.BL.Service.Interfaces;
using SupermarketApp.Data.Entities;
using SupermarketApp.Data.Repository.Interfaces;

namespace SupermarketApp.Core.Service
{
    public class DepartmentService : IDepartmentService
    {
        private readonly IRepository<Department> _repository;
        private readonly IRepository<Product> _productRepository;

        public DepartmentService(IRepository<Department> repository, IRepository<Product> productRepository)
        {
            _repository = repository;
            _productRepository = productRepository;
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

        public async Task<IEnumerable<Product>> GetProductsAsync()
        {
            return await _productRepository.GetAllAsync();
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
