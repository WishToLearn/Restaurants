using MediatR;
using Restaurants.Application.DTOs;

namespace Restaurants.Application.Queries.Restaurant
{
    public class GetAllRestaurantsQuery : IRequest<IEnumerable<RestaurantDTO>>
    {
    }
}
