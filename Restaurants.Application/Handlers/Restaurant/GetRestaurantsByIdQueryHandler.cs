using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Application.DTOs;
using Restaurants.Application.Queries.Restaurant;
using Restaurants.Domain.Exceptions;
using Restaurants.Domain.IRepositories;

namespace Restaurants.Application.Handlers.Restaurant
{
    public class GetRestaurantsByIdQueryHandler(ILogger<GetRestaurantsByIdQueryHandler> logger, IMapper mapper, IRestaurantsRepository restaurantsRepository) : IRequestHandler<GetRestaurantsByIdQuery, RestaurantDTO>
    {
        public async Task<RestaurantDTO> Handle(GetRestaurantsByIdQuery request, CancellationToken cancellationToken)
        {
            logger.LogInformation("Fetching teh Restaurant");

            var restaurant = await restaurantsRepository.GetAsync(request.Id) ?? throw new NotFoundException(nameof(Restaurants.Domain.Entities.Restaurant), request.Id.ToString());

            return mapper.Map<RestaurantDTO>(restaurant);
        }
    }
}
