using AutoMapper;
using SupermarketApp.Data.Entities;
using SupermarketApp.Core.Models;

namespace SupermarketApp.Core.Mapper
{
    public class ProductProfile : Profile
    {
        public ProductProfile()
        {
            CreateMap<ProductModel, Product>()
                .ForMember(p => p.Id, opt => opt.MapFrom(pm => pm.Id))
                .ForMember(p => p.Name, opt => opt.MapFrom(pm => pm.Name))
                .ForMember(p => p.Description, opt => opt.MapFrom(pm => pm.Description))
                .ForMember(p => p.Price, opt => opt.MapFrom(pm => pm.Price))
                .ForMember(p => p.Amount, opt => opt.MapFrom(pm => pm.Amount))
                .ForMember(p => p.ExpirationDate, opt => opt.MapFrom(pm => pm.ExpirationDate))
                .ForMember(p => p.CreationDate, opt => opt.MapFrom(pm => pm.CreationDate))
                .ForMember(p => p.Department, opt => opt.MapFrom(pm => pm.Department))
                .ForMember(p => p.Manufacturer, opt => opt.MapFrom(pm => pm.Manufacturer))
                .ForMember(p => p.DepartmentId, opt => opt.MapFrom(pm => pm.DepartmentId))
                .ForMember(p => p.Image, opt => opt.MapFrom(pm => pm.ImageFile != null ? ImageConvertor.ImageToString(pm.ImageFile) : pm.Image));

            CreateMap<Product, ProductModel>()
                .ForMember(pm => pm.Id, opt => opt.MapFrom(p => p.Id))
                .ForMember(pm => pm.Name, opt => opt.MapFrom(p => p.Name))
                .ForMember(pm => pm.Description, opt => opt.MapFrom(p => p.Description))
                .ForMember(pm => pm.Price, opt => opt.MapFrom(p => p.Price))
                .ForMember(pm => pm.Amount, opt => opt.MapFrom(p => p.Amount))
                .ForMember(pm => pm.ExpirationDate, opt => opt.MapFrom(p => p.ExpirationDate))
                .ForMember(pm => pm.CreationDate, opt => opt.MapFrom(p => p.CreationDate))
                .ForMember(pm => pm.Department, opt => opt.MapFrom(p => p.Department))
                .ForMember(pm => pm.Manufacturer, opt => opt.MapFrom(p => p.Manufacturer))
                .ForMember(pm => pm.DepartmentId, opt => opt.MapFrom(p => p.DepartmentId))
                .ForMember(pm => pm.Image, opt => opt.MapFrom(p => p.Image));
        }
    }
}
