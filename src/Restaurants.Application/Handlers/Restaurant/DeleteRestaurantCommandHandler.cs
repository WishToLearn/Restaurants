using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Application.Commands.Resraurant;
using Restaurants.Domain.Constants;
using Restaurants.Domain.Exceptions;
using Restaurants.Domain.Interfaces;
using Restaurants.Domain.IRepositories;

namespace Restaurants.Application.Handlers.Restaurant
{
    public class DeleteRestaurantCommandHandler(ILogger<DeleteRestaurantCommandHandler> logger, IRestaurantsRepository restaurantsRepository,
        IRestaurantAuthorizationService restaurantAuthorizationService) : IRequestHandler<DeleteRestaurantCommand>
    { 
        public async Task Handle(DeleteRestaurantCommand request, CancellationToken cancellationToken)
        {
            logger.LogInformation($"Deleting a Restaurant with restaurantId: {request.Id}");

            var restaurant = await restaurantsRepository.GetAsync(request.Id);

            if (restaurant is null)
            {
                throw new NotFoundException(nameof(Restaurants.Domain.Entities.Restaurant), request.Id.ToString());
            }

            var isAuthorized = restaurantAuthorizationService.IsAuthorize(restaurant, ResourceOperation.Delete);
            
            if (!isAuthorized)
            {
                throw new ForbidException();
            }

            await restaurantsRepository.DeleteAsync(restaurant);
        }
    }
}
