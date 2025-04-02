using AutoMapper;
using Restaurants.Application.Commands.Dish;
using Restaurants.Application.DTOs;
using Restaurants.Domain.Entities;

namespace Restaurants.Application.Mappings
{
    public class DishProfile : Profile
    {
        public DishProfile()
        {
            CreateMap<Dish, DishDTO>();

            CreateMap<CreateDishCommand, Dish>();
        }
    }
}
