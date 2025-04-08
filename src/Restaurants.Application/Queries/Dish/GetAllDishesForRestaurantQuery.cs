using MediatR;
using Restaurants.Application.DTOs;

namespace Restaurants.Application.Queries.Dish
{
    public class GetAllDishesForRestaurantQuery(int id) : IRequest<IEnumerable<DishDTO>>
    {
        public int RestaurantId { get; set; } = id;
    }
}
