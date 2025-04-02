using Restaurants.Domain.Entities;

namespace Restaurants.Domain.IRepositories
{
    public interface IDishesRepository
    {
        Task<int> CreateAsync(Dish dish);
        Task DeleteAllAsync(IEnumerable<Dish> dishes);
    }
}
