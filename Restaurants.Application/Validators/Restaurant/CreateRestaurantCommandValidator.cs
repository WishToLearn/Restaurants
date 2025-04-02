using FluentValidation;
using Restaurants.Application.Commands.Resraurant;

namespace Restaurants.Application.Validators.Restaurant
{
    public class CreateRestaurantCommandValidator : AbstractValidator<CreateRestaurantCommand>
    {
        private readonly List<string>  _validCategories = new List<string>() { "Indian", "Mexican", "American", "Italian", "Afghan", "Bakery", "Chinese" };
        public CreateRestaurantCommandValidator()
        {
            RuleFor(restaurant => restaurant.Name).
                Length(3, 100).
                WithMessage("Name should be 3 to 100 characters long");

            RuleFor(restaurant => restaurant.Description).
                NotEmpty().
                WithMessage("Description cannot be empty");

            RuleFor(restaurant => restaurant.Category).
                Must(_validCategories.Contains).
                WithMessage("Invalid category, Please choose from a valid category");

            RuleFor(restaurant => restaurant.ContactEmail).
                EmailAddress().
                WithMessage("Please provide a valid email address");

            RuleFor(restaurant => restaurant.PostalCode).
                Matches(@"^\d{2}-\d{3}$").
                WithMessage("Please provide a valid postal code (XX-XXX)");
        }
    }
}
