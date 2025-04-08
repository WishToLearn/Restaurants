using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Application.DTOs;
using Restaurants.Application.Queries.Dish;
using Restaurants.Domain.Exceptions;
using Restaurants.Domain.IRepositories;

namespace Restaurants.Application.Handlers.Dish
{
    public class GetAllDishedForRestaurantQueryHandler(ILogger<GetAllDishedForRestaurantQueryHandler> logger, IRestaurantsRepository restaurantsRepository, IMapper mapper) : IRequestHandler<GetAllDishesForRestaurantQuery, IEnumerable<DishDTO>>
    {
        public async Task<IEnumerable<DishDTO>> Handle(GetAllDishesForRestaurantQuery request, CancellationToken cancellationToken)
        {
            logger.LogInformation($"Getting dished for the Restaurant with Id: {request.RestaurantId}");

            var restaurant = await restaurantsRepository.GetAsync(request.RestaurantId);

            if( restaurant is null )
            {
                throw new NotFoundException(nameof(Restaurants.Domain.Entities.Restaurant), request.RestaurantId.ToString());
            }

            var dishes = mapper.Map<IEnumerable<DishDTO>>(restaurant.Dishes);

            return dishes;
        }
    }
}
