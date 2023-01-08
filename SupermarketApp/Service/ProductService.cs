using System.Linq.Expressions;
using SupermarketApp.Data.Repository;
using SupermarketApp.Models;
using SupermarketApp.Service.Interfaces;

namespace SupermarketApp.Service
{
    public class ProductService : IProductService
    {
        private readonly IRepository<Product> _repository;
        private readonly IRepository<Department> _departmentResository;
        private readonly IRepository<Manufacturer> _manufacturerResository;
        public ProductService(IRepository<Product> repository, IRepository<Department> depRep, IRepository<Manufacturer> manRep)
        {
            _repository = repository;
            _departmentResository = depRep;
            _manufacturerResository = manRep;
        }

        public async Task CreateProductAsync(Product product)
        {
            await _repository.CreateAsync(product);
        }

        public async Task<Product> FindProductByIdAsync(int id)
        {
            return await _repository.FindByIdAsync(id);
        }

        public async Task<IEnumerable<Product>> GetProductsAsync()
        {
            return await _repository.GetAllAsync();
        }

        public async Task<IEnumerable<Product>> GetProductsAsync(Func<Product, bool> predicate)
        {
            return await _repository.GetAllAsync(predicate);
        }

        public async Task RemoveProductAsync(Product product)
        {
            await _repository.RemoveAsync(product);
        }

        public async Task UpdateProductAsync(Product product)
        {
            await _repository.UpdateAsync(product);
        }

        public async Task<IEnumerable<Product>> GetProductsWithIncludeAsync(params Expression<Func<Product, object>>[] includeProperties)
        {
            return await _repository.GetAllWithIncludeAsync(includeProperties);
        }

        public async Task<Product> FindProductByIdWithIncludeAsync(int id, params Expression<Func<Product, object>>[] includeProperties)
        {
            return await _repository.FindByIdWithIncludeAsync(id, includeProperties);
        }

        public async Task<IEnumerable<Department>> GetDepartmentsAsync()
        {
            return await _departmentResository.GetAllAsync();
        }

        public async Task<IEnumerable<Manufacturer>> GetManufacturersAsync()
        {
            return await _manufacturerResository.GetAllAsync();
        }
    }
}
