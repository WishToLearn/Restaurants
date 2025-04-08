using Restaurants.Domain.Entities;
using Restaurants.Domain.IRepositories;
using Restaurants.Infrastructure.Persistance;

namespace Restaurants.Infrastructure.Repositories
{
    internal class DishesRespository(RestaurantsDbContext context) : IDishesRepository
    {
        public async Task<int> CreateAsync(Dish dish)
        {
            context.Dishes.Add(dish);

            await context.SaveChangesAsync();

            return dish.Id;
        }

        public async Task DeleteAllAsync(IEnumerable<Dish> dishes)
        {
            context.Dishes.RemoveRange(dishes);
            await context.SaveChangesAsync();
        }
    }
}
