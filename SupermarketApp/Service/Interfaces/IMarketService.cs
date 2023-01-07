using SupermarketApp.Models;

namespace SupermarketApp.Service.Interfaces
{
    public interface IMarketService
    {
        Task CreateDepartmentAsync(Department department);
        Task<Department> FindDepartmentByIdAsync(int id);
        Task<IEnumerable<Department>> GetDepartmentsAsync();
        Task<IEnumerable<Department>> GetDepartmentsAsync(Func<Department, bool> predicate);
        Task RemoveDepartmentAsync(Department department);
        Task UpdateDepartmentAsync(Department department);

        Task CreateManufacturerAsync(Manufacturer manufacturer);
        Task<Manufacturer> FindManufacturerByIdAsync(int id);
        Task<IEnumerable<Manufacturer>> GetManufacturersAsync();
        Task<IEnumerable<Manufacturer>> GetManufacturersAsync(Func<Manufacturer, bool> predicate);
        Task RemoveManufacturerAsync(Manufacturer manufacturer);
        Task UpdateManufacturerAsync(Manufacturer manufacturer);

        Task CreateProductAsync(Product product);
        Task<Product> FindProductByIdAsync(int id);
        Task<IEnumerable<Product>> GetProductsAsync();
        Task<IEnumerable<Product>> GetProductsAsync(Func<Product, bool> predicate);
        Task RemoveProductAsync(Product product);
        Task UpdateProductAsync(Product product);
    }
}
