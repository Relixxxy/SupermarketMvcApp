using AutoMapper;
using SupermarketApp.Data.Entities;
using SupermarketApp.Core.Models;

namespace SupermarketApp.Core.Mapper
{
    public class DepartmentProfile : Profile
    {
        public DepartmentProfile()
        {
            CreateMap<DepartmentModel, Department>()
                .ForMember(d => d.Id, opt => opt.MapFrom(dm => dm.Id))
                .ForMember(d => d.Name, opt => opt.MapFrom(dm => dm.Name))
                .ForMember(d => d.Description, opt => opt.MapFrom(dm => dm.Description))
                .ForMember(d => d.Image, opt => opt.MapFrom(dm => dm.ImageFile != null ? ImageConvertor.ImageToString(dm.ImageFile) : dm.Image));

            CreateMap<Department, DepartmentModel>()
                .ForMember(dm => dm.Id, opt => opt.MapFrom(d => d.Id))
                .ForMember(dm => dm.Name, opt => opt.MapFrom(d => d.Name))
                .ForMember(dm => dm.Description, opt => opt.MapFrom(d => d.Description))
                .ForMember(dm => dm.Image, opt => opt.MapFrom(d => d.Image));
        }
    }
}
