using MediatR;

namespace Restaurants.Application.Commands.Resraurant
{
    public class DeleteRestaurantCommand : IRequest
    {
        public DeleteRestaurantCommand(int id)
        {
            Id = id;
        }

        public int Id { get; set; }
    }
}
