using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Application.Commands.Dish;
using Restaurants.Domain.Constants;
using Restaurants.Domain.Exceptions;
using Restaurants.Domain.Interfaces;
using Restaurants.Domain.IRepositories;

namespace Restaurants.Application.Handlers.Dish
{
    public class DeleteAllDishesForRestaurantCommandHandler(ILogger<DeleteAllDishesForRestaurantCommandHandler> logger, IRestaurantsRepository restaurantsRepository, IDishesRepository dishesRepository, IRestaurantAuthorizationService restaurantAuthorizationService) : IRequestHandler<DeleteAllDishesForRestaurantCommand>
    {
        async Task IRequestHandler<DeleteAllDishesForRestaurantCommand>.Handle(DeleteAllDishesForRestaurantCommand request, CancellationToken cancellationToken)
        {
            logger.LogInformation($"Deleting dishes for the restaurant with Id: {request.RestaurantId}");

            var restaurant = await restaurantsRepository.GetAsync(request.RestaurantId);

            if (restaurant is null)
            {
                throw new NotFoundException(nameof(Restaurants.Domain.Entities.Restaurant), request.RestaurantId.ToString());
            }

            var isAuthorized = restaurantAuthorizationService.IsAuthorize(restaurant, ResourceOperation.Update);

            if(!isAuthorized)
            {
                throw new ForbidException();
            }

            await dishesRepository.DeleteAllAsync(restaurant.Dishes);
        }
    }
}
