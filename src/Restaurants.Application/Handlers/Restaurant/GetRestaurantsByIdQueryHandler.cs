using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Application.DTOs;
using Restaurants.Application.Queries.Restaurant;
using Restaurants.Domain.Exceptions;
using Restaurants.Domain.Interfaces;
using Restaurants.Domain.IRepositories;

namespace Restaurants.Application.Handlers.Restaurant
{
    public class GetRestaurantsByIdQueryHandler(ILogger<GetRestaurantsByIdQueryHandler> logger, IMapper mapper, IRestaurantsRepository restaurantsRepository, IBlobStorageService blobStorageService) : IRequestHandler<GetRestaurantsByIdQuery, RestaurantDTO>
    {
        public async Task<RestaurantDTO> Handle(GetRestaurantsByIdQuery request, CancellationToken cancellationToken)
        {
            logger.LogInformation($"Fetching the Restaurant with restaurantId: {request.Id}");

            var restaurant = await restaurantsRepository.GetAsync(request.Id) ?? throw new NotFoundException(nameof(Restaurants.Domain.Entities.Restaurant), request.Id.ToString());

            var restaurantDTO = mapper.Map<RestaurantDTO>(restaurant);

            restaurantDTO.LogoSasUrl = blobStorageService.GetBlobSasUrl(restaurant.LogoUrl);

            return restaurantDTO;
        }
    }
}
