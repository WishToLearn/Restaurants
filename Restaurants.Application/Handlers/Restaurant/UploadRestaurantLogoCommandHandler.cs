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
    internal class UploadRestaurantLogoCommandHandler(ILogger<UploadRestaurantLogoCommandHandler> logger, IRestaurantsRepository restaurantsRepository, IRestaurantAuthorizationService restaurantAuthorizationService, IBlobStorageService blobStorageService) : IRequestHandler<UploadRestaurantLogoCommand>
    {
        public async Task Handle(UploadRestaurantLogoCommand request, CancellationToken cancellationToken)
        {
            logger.LogInformation("Updating the logo for the Restaurant: {RestaurantId}", request.RestaurantId);

            var restaurant = await restaurantsRepository.GetAsync(request.RestaurantId);

            if (restaurant is null)
            {
                throw new NotFoundException(nameof(Restaurants.Domain.Entities.Restaurant), request.RestaurantId.ToString());
            }

            var isAuthorizedToUpload = restaurantAuthorizationService.IsAuthorize(restaurant, ResourceOperation.Update);

            if (!isAuthorizedToUpload)
            {
                throw new ForbidException();
            }

            var logoUrl = await blobStorageService.UploadToBlobAsync(request.File, request.FileName);

            logger.LogInformation("Uploaded the logo for the Restaurant: {RestaurantId}", request.RestaurantId);
            
            restaurant.LogoUrl = logoUrl;

            await restaurantsRepository.UpdateAsync(restaurant);
        }
    }
}
