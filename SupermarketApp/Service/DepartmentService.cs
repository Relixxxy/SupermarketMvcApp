using AutoMapper;
using SupermarketApp.Core.Service.Interfaces;
using SupermarketApp.Core.Mapper;
using SupermarketApp.Data.Entities;
using SupermarketApp.Core.Models;
using SupermarketApp.Data.Repository.Interfaces;

namespace SupermarketApp.Core.Service
{
    public class DepartmentService : IDepartmentService
    {
        private readonly IRepository<Department> _repository;
        private readonly IRepository<Product> _productRepository;
        private readonly IMapper _mapper;

        public DepartmentService(IRepository<Department> repository, IRepository<Product> productRepository, IMapper mapper)
        {
            _repository = repository;
            _productRepository = productRepository;
            _mapper = mapper;
        }

        public async Task CreateDepartmentAsync(DepartmentModel departmentModel)
        {
            if (departmentModel.ImageFile == null)
            {
                departmentModel.Image = ImageConvertor.NOIMAGE;
            }

            var department = _mapper.Map<Department>(departmentModel);
            await _repository.CreateAsync(department);
        }

        public async Task<DepartmentModel> FindDepartmentByIdAsync(int id)
        {
            var department = await _repository.FindByIdAsync(id);
            var departmentModel = _mapper.Map<DepartmentModel>(department);
            return departmentModel;
        }

        public async Task<IEnumerable<DepartmentModel>> GetDepartmentsAsync()
        {
            var departments = await _repository.GetAllAsync();
            var departmentModels = departments.Select(_mapper.Map<DepartmentModel>).ToList();
            return departmentModels;
        }

        public async Task<IEnumerable<ProductModel>> GetProductsAsync()
        {
            var products = await _productRepository.GetAllAsync();
            var productModels = products.Select(_mapper.Map<ProductModel>).ToList();
            return productModels;
        }

        public async Task RemoveDepartmentAsync(DepartmentModel departmentModel)
        {
            var department = _mapper.Map<Department>(departmentModel);
            await _repository.RemoveAsync(department);
        }

        public async Task UpdateDepartmentAsync(DepartmentModel departmentModel)
        {
            var department = _mapper.Map<Department>(departmentModel);
            await _repository.UpdateAsync(department);
        }
    }
}
