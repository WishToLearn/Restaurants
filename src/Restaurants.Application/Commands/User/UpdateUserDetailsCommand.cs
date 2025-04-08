using MediatR;

namespace Restaurants.Application.Commands.User
{
    public class UpdateUserDetailsCommand : IRequest
    {
        public DateOnly? DateOfBirth { get; set; }
        public string? Nationality { get; set; }
    }
}
