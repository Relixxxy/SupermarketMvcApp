using System.Linq.Expressions;
using SupermarketApp.Models;

namespace SupermarketApp.Service.Interfaces
{
    public interface IProductService
    {
        Task CreateProductAsync(Product product);
        Task<Product> FindProductByIdAsync(int id);
        Task<IEnumerable<Product>> GetProductsAsync();
        Task RemoveProductAsync(Product product);
        Task UpdateProductAsync(Product product);
        Task<IEnumerable<Product>> GetProductsWithIncludeAsync(params Expression<Func<Product, object>>[] includeProperties);
        Task<Product> FindProductByIdWithIncludeAsync(int id, params Expression<Func<Product, object>>[] includeProperties);
        Task<IEnumerable<Department>> GetDepartmentsAsync();
        Task<IEnumerable<Manufacturer>> GetManufacturersAsync();
    }
}
