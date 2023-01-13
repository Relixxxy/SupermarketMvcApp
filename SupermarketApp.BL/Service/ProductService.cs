using AutoMapper;
using SupermarketApp.BL.Service.Interfaces;
using SupermarketApp.Data.Entities;
using SupermarketApp.Data.Mapper;
using SupermarketApp.Data.Models;
using SupermarketApp.Data.Repository.Interfaces;

namespace SupermarketApp.Core.Service
{
    public class ProductService : IProductService
    {
        private readonly IRepository<Product> _repository;
        private readonly IMapper _mapper;
        public ProductService(IRepository<Product> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task CreateProductAsync(ProductModel productModel)
        {
            if (productModel.ImageFile == null)
            {
                productModel.Image = ImageConvertor.NOIMAGE;
            }

            var product = _mapper.Map<Product>(productModel);
            await _repository.CreateAsync(product);
        }

        public async Task<ProductModel> FindProductByIdAsync(int id)
        {
            var product = await _repository.FindByIdAsync(id);
            var prodcutModel = _mapper.Map<ProductModel>(product);
            return prodcutModel;
        }

        public async Task<IEnumerable<ProductModel>> GetProductsAsync()
        {
            var products = await _repository.GetAllAsync();
            var productModels = products.Select(_mapper.Map<ProductModel>);
            return productModels;
        }

        public async Task RemoveProductAsync(ProductModel productModel)
        {
            var product = _mapper.Map<Product>(productModel);
            await _repository.RemoveAsync(product);
        }

        public async Task UpdateProductAsync(ProductModel productModel)
        {
            var product = _mapper.Map<Product>(productModel);
            await _repository.UpdateAsync(product);
        }
    }
}
