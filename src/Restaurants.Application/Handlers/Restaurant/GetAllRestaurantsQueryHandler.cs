using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.API.Common;
using Restaurants.Application.DTOs;
using Restaurants.Application.Queries.Restaurant;
using Restaurants.Domain.IRepositories;

namespace Restaurants.Application.Handlers.Restaurant
{
    public class GetAllRestaurantsQueryHandler(ILogger<GetAllRestaurantsQueryHandler> logger, IMapper mapper, IRestaurantsRepository restaurantsRepository) : IRequestHandler<GetAllRestaurantsQuery, PagedResult<RestaurantDTO>>
    {
        public async Task<PagedResult<RestaurantDTO>> Handle(GetAllRestaurantsQuery request, CancellationToken cancellationToken)
        {
            logger.LogInformation("Fetching Restaurants");

            var (restaurants, totalCount) = await restaurantsRepository.GetAllAsync(request.SearchPhrase, request.PageNumber, request.PageSize, request.SortBy, request.SortDirection);

            var restaurantDTOs = mapper.Map<IEnumerable<RestaurantDTO>>(restaurants);

            var result = new PagedResult<RestaurantDTO>(restaurantDTOs, totalCount, request.PageNumber, request.PageSize);

            return result;
        }
    }
}
