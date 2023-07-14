using AutoMapper;

namespace MyPetProject.Profiles
{
    public class OrderProfile : Profile
    {
        public OrderProfile()
        {
            CreateMap<Models.Domain.Order, Models.DTO.Order>()
                .ReverseMap();
            CreateMap<Models.Domain.OrderProduct, Models.DTO.OrderProduct>()
                .ReverseMap();
            CreateMap<Models.Domain.OrderProduct, Models.DTO.AddOrderProductRequest>()
                .ReverseMap();
        }
    }
}
