using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Application.Commands.Dish;
using Restaurants.Domain.Constants;
using Restaurants.Domain.Exceptions;
using Restaurants.Domain.Interfaces;
using Restaurants.Domain.IRepositories;

namespace Restaurants.Application.Handlers.Dish
{
    public class CreateDishCommandHandler(ILogger<CreateDishCommandHandler> logger, IRestaurantsRepository restaurantsRepository, IMapper mapper, IDishesRepository dishesRepository, IRestaurantAuthorizationService restaurantAuthorizationService) : IRequestHandler<CreateDishCommand, int>
    {
        public async Task<int> Handle(CreateDishCommand request, CancellationToken cancellationToken)
        {
            logger.LogInformation("Creating a dish for a restaurant");

            var restaurant = await restaurantsRepository.GetAsync(request.RestaurantId);

            if (restaurant is null)
            {
                throw new NotFoundException(nameof(Restaurant), request.RestaurantId.ToString());
            }

            var isAuthorized = restaurantAuthorizationService.IsAuthorize(restaurant, ResourceOperation.Update);

            if (!isAuthorized)
            {
                throw new ForbidException();
            }

            var dish = mapper.Map<Restaurants.Domain.Entities.Dish>(request);

            var dishId = await dishesRepository.CreateAsync(dish);

            return dishId;
        }
    }
}
