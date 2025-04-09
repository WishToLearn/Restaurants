using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Application.Commands.Resraurant;
using Restaurants.Domain.Constants;
using Restaurants.Domain.Exceptions;
using Restaurants.Domain.Interfaces;
using Restaurants.Domain.IRepositories;

namespace Restaurants.Application.Handlers.Restaurant
{
    public class UpdateRestaurantCommandHandler(ILogger<UpdateRestaurantCommandHandler> logger, IMapper mapper, IRestaurantsRepository restaurantsRepository, IRestaurantAuthorizationService restaurantAuthorizationService) : IRequestHandler<UpdateRestaurantCommand>
    {
        public async Task Handle(UpdateRestaurantCommand request, CancellationToken cancellationToken)
        {
            var restaurant = await restaurantsRepository.GetAsync(request.Id);

            if (restaurant is null)
            {
                throw new NotFoundException(nameof(Restaurants.Domain.Entities.Restaurant), request.Id.ToString());
            }

            var isAuthorized = restaurantAuthorizationService.IsAuthorize(restaurant, ResourceOperation.Update);

            if (!isAuthorized)
            {
                throw new ForbidException();
            }

            mapper.Map(request, restaurant);

            logger.LogInformation("Updating the {@Rstaurant}", restaurant);

            await restaurantsRepository.UpdateAsync(restaurant);
        }
    }
}
