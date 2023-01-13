using SupermarketApp.BL.Service.Interfaces;
using SupermarketApp.Data.Entities;
using SupermarketApp.Data.Repository.Interfaces;

namespace SupermarketApp.Core.Service
{
    public class ProductService : IProductService
    {
        private readonly IRepository<Product> _repository;
        public ProductService(IRepository<Product> repository)
        {
            _repository = repository;
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

        public async Task RemoveProductAsync(Product product)
        {
            await _repository.RemoveAsync(product);
        }

        public async Task UpdateProductAsync(Product product)
        {
            await _repository.UpdateAsync(product);
        }
    }
}
