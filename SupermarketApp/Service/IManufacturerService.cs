using SupermarketApp.Models;

namespace SupermarketApp.Service
{
    public interface IManufacturerService
    {
        Task<IEnumerable<Manufacturer>> GetManufacturersAsync();
    }
}
