using FluentValidation;
using Restaurants.Application.Commands.Resraurant;

namespace Restaurants.Application.Validators.Restaurant
{
    public class UpdateRestaurantCommandValidator : AbstractValidator<UpdateRestaurantCommand>
    {
        public UpdateRestaurantCommandValidator()
        {
            RuleFor(restaurant => restaurant.Name).
                Length(3, 100).
                WithMessage("Name should be 3 to 100 characters long");

            RuleFor(restaurant => restaurant.Description).
                NotEmpty().
                WithMessage("Description cannot be empty");
        }
    }
}
