using AutoMapper;

namespace MyPetProject.Profiles
{
    public class OrderStatusType : Profile
    {
        public OrderStatusType()
        {
            CreateMap<Models.Domain.OrderStatus, Models.DTO.OrderStatus>()
                .ReverseMap();
        }
    }
}
