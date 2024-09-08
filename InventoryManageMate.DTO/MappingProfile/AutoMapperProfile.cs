using AutoMapper;
using InventoryManageMate.DTO.DTOs;
using InventoryManageMate.DTO.Models;

namespace InventoryManageMate.DTO.MappingProfile
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Order, OrderDto>().ReverseMap();
            CreateMap<OrderDetail, OrderDetailDto>().ReverseMap();
        }
    }
}

