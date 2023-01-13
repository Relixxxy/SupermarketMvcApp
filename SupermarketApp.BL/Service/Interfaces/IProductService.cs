using System.Linq.Expressions;
using SupermarketApp.Data.Entities;

namespace SupermarketApp.BL.Service.Interfaces
{
    public interface IProductService
    {
        Task CreateProductAsync(Product product);
        Task<Product> FindProductByIdAsync(int id);
        Task<IEnumerable<Product>> GetProductsAsync();
        Task RemoveProductAsync(Product product);
        Task UpdateProductAsync(Product product);
    }
}
