using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Application.DTOs;
using Restaurants.Application.Queries.Dish;
using Restaurants.Domain.Exceptions;
using Restaurants.Domain.IRepositories;

namespace Restaurants.Application.Handlers.Dish
{
    public class GetDishForRestaurantQueryHandler(ILogger<GetDishForRestaurantQueryHandler> logger, IRestaurantsRepository restaurantsRepository, IMapper mapper) : IRequestHandler<GetDishForRestaurantQuery, DishDTO>
    {
        public async Task<DishDTO> Handle(GetDishForRestaurantQuery request, CancellationToken cancellationToken)
        {
            logger.LogInformation($"Getting a dish with dishId: {request.DishId} for the Restaurant with Id: {request.RestaurantId}");

            var restaurant = await restaurantsRepository.GetAsync(request.RestaurantId);

            if (restaurant is null)
            {
                throw new NotFoundException(nameof(Restaurants.Domain.Entities.Restaurant), request.RestaurantId.ToString());
            }

            var dish = restaurant.Dishes.FirstOrDefault(dish => dish.Id == request.DishId);

            if (dish is null)
            {
                throw new NotFoundException(nameof(Restaurants.Domain.Entities.Dish), request.DishId.ToString());
            }

            var dishDTO = mapper.Map<DishDTO>(dish);

            return dishDTO;
        }
    }
}
