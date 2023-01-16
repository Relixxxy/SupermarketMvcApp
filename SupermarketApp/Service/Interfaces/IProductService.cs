using SupermarketApp.Core.Models;

namespace SupermarketApp.Core.Service.Interfaces
{
    public interface IProductService
    {
        Task CreateProductAsync(ProductModel productModel);
        Task<ProductModel> FindProductByIdAsync(int id);
        Task<IEnumerable<ProductModel>> GetProductsAsync();
        Task RemoveProductAsync(ProductModel productModel);
        Task UpdateProductAsync(ProductModel productModel);
    }
}
