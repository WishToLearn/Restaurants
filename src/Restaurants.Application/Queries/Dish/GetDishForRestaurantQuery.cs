using MediatR;
using Restaurants.Application.DTOs;

namespace Restaurants.Application.Queries.Dish
{
    public class GetDishForRestaurantQuery(int restaurantId, int dishId) : IRequest<DishDTO>
    {
        public int RestaurantId { get; set; } = restaurantId;
        public int DishId { get; set; } = dishId;
    }
}
