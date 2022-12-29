using SupermarketApp.Models;

namespace SupermarketApp.Service
{
    public interface IProductService
    {
        Task<IEnumerable<Product>> GetProductsAsync();
    }
}
