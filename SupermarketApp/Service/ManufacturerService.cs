using SupermarketApp.Data.Repository;
using SupermarketApp.Models;
using SupermarketApp.Service.Interfaces;

namespace SupermarketApp.Service
{
    public class ManufacturerService : IManufacturerService
    {
        private readonly IRepository<Manufacturer> _repository;

        public ManufacturerService(IRepository<Manufacturer> repository)
        {
            _repository = repository;
        }

        public async Task CreateManufacturerAsync(Manufacturer manufacturer)
        {
            await _repository.CreateAsync(manufacturer);
        }

        public async Task<Manufacturer> FindManufacturerByIdAsync(int id)
        {
            return await _repository.FindByIdAsync(id);
        }

        public async Task<IEnumerable<Manufacturer>> GetManufacturersAsync()
        {
            return await _repository.GetAllAsync();
        }

        public async Task RemoveManufacturerAsync(Manufacturer manufacturer)
        {
            await _repository.RemoveAsync(manufacturer);
        }

        public async Task UpdateManufacturerAsync(Manufacturer manufacturer)
        {
            await _repository.UpdateAsync(manufacturer);
        }
    }
}
