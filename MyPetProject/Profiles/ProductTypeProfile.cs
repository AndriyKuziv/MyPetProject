using AutoMapper;

namespace MyPetProject.Profiles
{
    public class ProductTypeProfile : Profile
    {
        public ProductTypeProfile()
        {
            CreateMap<Models.Domain.ProductType, Models.DTO.ProductType>()
                .ReverseMap();
        }
    }
}
