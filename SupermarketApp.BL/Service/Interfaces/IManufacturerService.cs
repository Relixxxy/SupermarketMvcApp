using SupermarketApp.Data.Models;

namespace SupermarketApp.BL.Service.Interfaces
{
    public interface IManufacturerService
    {
        Task CreateManufacturerAsync(ManufacturerModel manufacturer);
        Task<ManufacturerModel> FindManufacturerByIdAsync(int id);
        Task<IEnumerable<ManufacturerModel>> GetManufacturersAsync();
        Task RemoveManufacturerAsync(ManufacturerModel manufacturer);
        Task UpdateManufacturerAsync(ManufacturerModel manufacturer);
    }
}
