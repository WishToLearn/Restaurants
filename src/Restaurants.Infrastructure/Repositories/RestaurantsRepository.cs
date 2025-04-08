using Microsoft.EntityFrameworkCore;
using Restaurants.Domain.Common;
using Restaurants.Domain.Entities;
using Restaurants.Domain.IRepositories;
using Restaurants.Infrastructure.Persistance;
using System.Linq.Expressions;

namespace Restaurants.Infrastructure.Repositories
{
    internal class RestaurantsRepository(RestaurantsDbContext context) : IRestaurantsRepository
    {
        public async Task<int> CreateAsync(Restaurant restaurant)
        {
            context.Restaurants.Add(restaurant);
            await context.SaveChangesAsync();
            return restaurant.Id;
        }

        public async Task<(IEnumerable<Restaurant>, int)> GetAllAsync(string? searchPhrase, int pageNumber, int pageSize, string? sortBy, SortDirection sortDirection)
        {
            var searchPhraseToLower = searchPhrase?.ToLower();

            var baseQuery = context
                .Restaurants
                .Where(restaurant
                    => searchPhrase == null
                    ||
                    restaurant.Name.ToLower().Contains(searchPhraseToLower!)
                    ||
                    restaurant.Description.ToLower().Contains(searchPhraseToLower!));

            var totalCount = await baseQuery.CountAsync();

            if(sortBy is not null)
            {
                var columnSelector = new Dictionary<string, Expression<Func<Restaurant, object>>>()
                {
                    { nameof(Restaurant.Name), restaurant => restaurant.Name },
                    { nameof(Restaurant.Category), restaurant => restaurant.Category },
                    { nameof(Restaurant.Description), restaurant => restaurant.Description }
                };

                var selectedColumn = columnSelector[sortBy];

                baseQuery = sortDirection == SortDirection.Ascending
                    ? baseQuery.OrderBy(selectedColumn)
                    : baseQuery.OrderByDescending(selectedColumn);
            }

            var restaurants = await baseQuery
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return (restaurants, totalCount);
        }

        public async Task<Restaurant?> GetAsync(int id)
        {
            var restaurant = await context.Restaurants.
                Include(restaurant => restaurant.Dishes).
                FirstOrDefaultAsync(restaurant => restaurant.Id ==id);

            return restaurant;
        }

        public async Task UpdateAsync(Restaurant restaurant)
        {
            context.Update(restaurant);
            await context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Restaurant restaurant)
        {
            context.Remove(restaurant);
            await context.SaveChangesAsync();
        }
    }
}
