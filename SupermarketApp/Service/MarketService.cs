using System.Linq.Expressions;
using SupermarketApp.Data.Repository;
using SupermarketApp.Models;

namespace SupermarketApp.Service
{
    public class MarketService : IMarketService
    {
        private readonly IRepository<Department> _departmentRepository;
        private readonly IRepository<Manufacturer> _manufacturerRepository;
        private readonly IRepository<Product> _productRepository;

        public MarketService(IRepository<Department> departmentRepository, IRepository<Manufacturer> manufacturerRepository, IRepository<Product> productRepository)
        {
            _departmentRepository = departmentRepository;
            _manufacturerRepository = manufacturerRepository;
            _productRepository = productRepository;
        }

        public async Task CreateDepartmentAsync(Department department)
        {
            await _departmentRepository.CreateAsync(department);
        }

        public async Task CreateManufacturerAsync(Manufacturer manufacturer)
        {
            await _manufacturerRepository.CreateAsync(manufacturer);
        }

        public async Task CreateProductAsync(Product product)
        {
            await _productRepository.CreateAsync(product);
        }

        public async Task<Department> FindDepartmentByIdAsync(int id)
        {
            return await _departmentRepository.FindByIdAsync(id);
        }

        public async Task<Manufacturer> FindManufacturerByIdAsync(int id)
        {
            return await _manufacturerRepository.FindByIdAsync(id);
        }

        public async Task<Product> FindProductByIdAsync(int id)
        {
            return await _productRepository.FindByIdAsync(id);
        }

        public async Task<IEnumerable<Department>> GetDepartmentsAsync()
        {
            return await _departmentRepository.GetAllAsync();
        }

        public async Task<IEnumerable<Department>> GetDepartmentsAsync(Func<Department, bool> predicate)
        {
            return await _departmentRepository.GetAllAsync(predicate);
        }

        public async Task<IEnumerable<Manufacturer>> GetManufacturersAsync()
        {
            return await _manufacturerRepository.GetAllAsync();
        }

        public async Task<IEnumerable<Manufacturer>> GetManufacturersAsync(Func<Manufacturer, bool> predicate)
        {
            return await _manufacturerRepository.GetAllAsync(predicate);
        }

        public async Task<IEnumerable<Product>> GetProductsAsync()
        {
            return await _productRepository.GetAllAsync();
        }

        public async Task<IEnumerable<Product>> GetProductsAsync(Func<Product, bool> predicate)
        {
            return await _productRepository.GetAllAsync(predicate);
        }

        public async Task RemoveDepartmentAsync(Department department)
        {
            await _departmentRepository.RemoveAsync(department);
        }

        public async Task RemoveManufacturerAsync(Manufacturer manufacturer)
        {
            await _manufacturerRepository.RemoveAsync(manufacturer);
        }

        public async Task RemoveProductAsync(Product product)
        {
            await _productRepository.RemoveAsync(product);
        }

        public async Task UpdateDepartmentAsync(Department department)
        {
            await _departmentRepository.UpdateAsync(department);
        }

        public async Task UpdateManufacturerAsync(Manufacturer manufacturer)
        {
            await _manufacturerRepository.UpdateAsync(manufacturer);
        }

        public async Task UpdateProductAsync(Product product)
        {
            await _productRepository.UpdateAsync(product);
        }

        public async Task<IEnumerable<Department>> GetDepartmentsWithIncludeAsync(params Expression<Func<Department, object>>[] includeProperties)
        {
            return await _departmentRepository.GetAllWithIncludeAsync(includeProperties);
        }

        public async Task<IEnumerable<Department>> GetDepartmentsWithIncludeAsync(
            Func<Department, bool> predicate,
            params Expression<Func<Department, object>>[] includeProperties)
        {
            return await _departmentRepository.GetAllWithIncludeAsync(predicate, includeProperties);
        }

        public async Task<IEnumerable<Manufacturer>> GetManufacturersWithIncludeAsync(params Expression<Func<Manufacturer, object>>[] includeProperties)
        {
            return await _manufacturerRepository.GetAllWithIncludeAsync(includeProperties);
        }

        public async Task<IEnumerable<Manufacturer>> GetManufacturersWithIncludeAsync(
            Func<Manufacturer, bool> predicate,
            params Expression<Func<Manufacturer, object>>[] includeProperties)
        {
            return await _manufacturerRepository.GetAllWithIncludeAsync(predicate, includeProperties);
        }

        public async Task<IEnumerable<Product>> GetProductsWithIncludeAsync(params Expression<Func<Product, object>>[] includeProperties)
        {
            return await _productRepository.GetAllWithIncludeAsync(includeProperties);
        }

        public async Task<IEnumerable<Product>> GetProductsWithIncludeAsync(
            Func<Product, bool> predicate,
            params Expression<Func<Product, object>>[] includeProperties)
        {
            return await _productRepository.GetAllWithIncludeAsync(predicate, includeProperties);
        }
    }
}
