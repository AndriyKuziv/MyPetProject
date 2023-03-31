﻿using AutoMapper;

namespace MyPetProject.Profiles
{
    public class OrderProfile : Profile
    {
        public OrderProfile()
        {
            CreateMap<Models.Domain.Order, Models.DTO.Order>()
                .ReverseMap();
        }
    }
}
