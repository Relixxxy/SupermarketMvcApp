using AutoMapper;
using SupermarketApp.Data.Entities;
using SupermarketApp.Data.Models;

namespace SupermarketApp.Data.Mapper
{
    public class ManufacturerProfile : Profile
    {
        public ManufacturerProfile()
        {
            CreateMap<ManufacturerModel, Manufacturer>()
                .ForMember(m => m.Id, opt => opt.MapFrom(mm => mm.Id))
                .ForMember(m => m.Name, opt => opt.MapFrom(mm => mm.Name))
                .ForMember(m => m.Image, opt => opt.MapFrom(mm => mm.ImageFile != null ? ImageConvertor.ImageToString(mm.ImageFile) : mm.Image))
                .ForMember(m => m.Products, opt => opt.MapFrom(mm => mm.Products));

            CreateMap<Manufacturer, ManufacturerModel>()
                .ForMember(mm => mm.Id, opt => opt.MapFrom(m => m.Id))
                .ForMember(mm => mm.Name, opt => opt.MapFrom(m => m.Name))
                .ForMember(mm => mm.Image, opt => opt.MapFrom(m => m.Image))
                .ForMember(mm => mm.Products, opt => opt.MapFrom(m => m.Products));
        }
    }
}
