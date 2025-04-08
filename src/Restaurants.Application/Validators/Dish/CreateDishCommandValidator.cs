using FluentValidation;
using Restaurants.Application.Commands.Dish;

namespace Restaurants.Application.Validators.Dish
{
    public class CreateDishCommandValidator : AbstractValidator<CreateDishCommand>
    {
        public CreateDishCommandValidator()
        {
            RuleFor(dish => dish.Price).
                GreaterThan(0).
                WithMessage("Price must be a non-negative number");

            RuleFor(dish => dish.KiloCalories).
                GreaterThan(0).
                WithMessage("Kilo calories must be a non-negative number");
        }
    }
}
