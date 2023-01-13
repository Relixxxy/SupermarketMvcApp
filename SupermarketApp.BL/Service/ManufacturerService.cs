using AutoMapper;
using SupermarketApp.BL.Service.Interfaces;
using SupermarketApp.Data.Entities;
using SupermarketApp.Data.Mapper;
using SupermarketApp.Data.Models;
using SupermarketApp.Data.Repository.Interfaces;

namespace SupermarketApp.Core.Service
{
    public class ManufacturerService : IManufacturerService
    {
        private readonly IRepository<Manufacturer> _repository;
        private readonly IMapper _mapper;

        public ManufacturerService(IRepository<Manufacturer> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task CreateManufacturerAsync(ManufacturerModel manufacturerModel)
        {
            if (manufacturerModel.ImageFile == null)
            {
                manufacturerModel.Image = ImageConvertor.NOIMAGE;
            }

            var manufacturer = _mapper.Map<Manufacturer>(manufacturerModel);
            await _repository.CreateAsync(manufacturer);
        }

        public async Task<ManufacturerModel> FindManufacturerByIdAsync(int id)
        {
            var manufacturer = await _repository.FindByIdAsync(id);
            var manufacturerModel = _mapper.Map<ManufacturerModel>(manufacturer);
            return manufacturerModel;
        }

        public async Task<IEnumerable<ManufacturerModel>> GetManufacturersAsync()
        {
            var manufacturers = await _repository.GetAllAsync();
            var manufacturerModels = manufacturers.Select(_mapper.Map<ManufacturerModel>);
            return manufacturerModels;
        }

        public async Task RemoveManufacturerAsync(ManufacturerModel manufacturerModel)
        {
            var manufacturer = _mapper.Map<Manufacturer>(manufacturerModel);
            await _repository.RemoveAsync(manufacturer);
        }

        public async Task UpdateManufacturerAsync(ManufacturerModel manufacturerModel)
        {
            var manufacturer = _mapper.Map<Manufacturer>(manufacturerModel);
            await _repository.UpdateAsync(manufacturer);
        }
    }
}
