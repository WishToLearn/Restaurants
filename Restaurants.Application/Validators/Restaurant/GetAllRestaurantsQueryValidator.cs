using FluentValidation;
using Restaurants.Application.DTOs;
using Restaurants.Application.Queries.Restaurant;

namespace Restaurants.Application.Validators.Restaurant
{
    internal class GetAllRestaurantsQueryValidator : AbstractValidator<GetAllRestaurantsQuery>
    {
        private int[] allowedPageSizes = new[] { 5, 10, 15, 30 };

        private string[] allowedSortBy = new[] { nameof(RestaurantDTO.Name), nameof(RestaurantDTO.Category), nameof(RestaurantDTO.Description) };

        public GetAllRestaurantsQueryValidator()
        {
            RuleFor(x => x.PageNumber)
                .GreaterThanOrEqualTo(1)
                .WithMessage("Page number must be greater than or equal to 1.");

            RuleFor(x => x.PageSize)
                .GreaterThanOrEqualTo(1)
                .WithMessage("Page size must be greater than or equal to 1.")
                .Must(size => allowedPageSizes.Contains(size))
                .WithMessage($"Page size must be in [{string.Join(",", allowedPageSizes)}]");

            RuleFor(x => x.SortBy)
                .Must(sortBy => allowedSortBy.Contains(sortBy))
                .When(query => query.SortBy is not null)
                .WithMessage($"SortBy must be in [{string.Join(",", allowedSortBy)}]");
        }
    }
}
