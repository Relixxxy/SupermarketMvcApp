using SupermarketApp.Models;

namespace SupermarketApp.Service.Interfaces
{
    public interface IManufacturerService
    {
        Task CreateManufacturerAsync(Manufacturer manufacturer);
        Task<Manufacturer> FindManufacturerByIdAsync(int id);
        Task<IEnumerable<Manufacturer>> GetManufacturersAsync();
        Task RemoveManufacturerAsync(Manufacturer manufacturer);
        Task UpdateManufacturerAsync(Manufacturer manufacturer);
    }
}
