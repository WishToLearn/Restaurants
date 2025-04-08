using MediatR;

namespace Restaurants.Application.Commands.Dish
{
    public class DeleteAllDishesForRestaurantCommand(int restaurantId) : IRequest
    {
        public int RestaurantId { get; set; } = restaurantId;
    }
}
