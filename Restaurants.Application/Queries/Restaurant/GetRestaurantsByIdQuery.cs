using MediatR;
using Restaurants.Application.DTOs;

namespace Restaurants.Application.Queries.Restaurant
{
    public  class GetRestaurantsByIdQuery : IRequest<RestaurantDTO>
    {
        public GetRestaurantsByIdQuery(int id)
        {
            Id = id;
        }
        public int Id { get; set; }
    }
}
