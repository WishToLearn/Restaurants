using Microsoft.EntityFrameworkCore;
using Restaurants.Domain.Entities;
using Restaurants.Domain.IRepositories;
using Restaurants.Infrastructure.Persistance;

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

        public async Task<IEnumerable<Restaurant>> GetAllAsync()
        {
            var restaurants = await context.Restaurants.ToListAsync();
            return restaurants;
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
