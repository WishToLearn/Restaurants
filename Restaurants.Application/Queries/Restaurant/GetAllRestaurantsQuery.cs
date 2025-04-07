using MediatR;
using Restaurants.API.Common;
using Restaurants.Application.DTOs;
using Restaurants.Domain.Common;

namespace Restaurants.Application.Queries.Restaurant
{
    public class GetAllRestaurantsQuery : IRequest<PagedResult<RestaurantDTO>>
    {
        public string? SearchPhrase { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public string? SortBy { get; set; }
        public SortDirection SortDirection { get; set; }
    }
}
