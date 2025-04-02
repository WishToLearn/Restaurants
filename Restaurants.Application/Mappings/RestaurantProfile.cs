using AutoMapper;
using Restaurants.Application.Commands.Resraurant;
using Restaurants.Application.DTOs;
using Restaurants.Domain.Entities;

namespace Restaurants.Application.Mappings
{
    public class RestaurantProfile : Profile
    {
        public RestaurantProfile()
        {
            CreateMap<CreateRestaurantCommand, Restaurant>().
                ForMember(dest => dest.Address, opt =>
                    opt.MapFrom(
                        src => new Address
                        {
                            City = src.City,
                            Street = src.Street,
                            PostalCode = src.PostalCode
                        }));

            CreateMap<UpdateRestaurantCommand, Restaurant>();

            CreateMap<Restaurant, RestaurantDTO>().
                ForMember(dest => dest.City, opt =>
                    opt.MapFrom(src => src.Address == null ? null : src.Address.City)).
                ForMember(dest => dest.Street, opt =>
                    opt.MapFrom(src => src.Address == null ? null : src.Address.Street)).
                ForMember(dest => dest.PostalCode, opt =>
                    opt.MapFrom(src => src.Address == null ? null : src.Address.PostalCode)).
                ForMember(dest => dest.Dishes, opt =>
                    opt.MapFrom(src => src.Dishes));
        }
    }
}
