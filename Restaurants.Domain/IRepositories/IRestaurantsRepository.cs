using Restaurants.Domain.Entities;

namespace Restaurants.Domain.IRepositories
{
    public interface IRestaurantsRepository
    {
        Task<int> CreateAsync(Restaurant restaurant);
        Task<IEnumerable<Restaurant>> GetAllAsync();
        Task<Restaurant?> GetAsync(int id);
        Task UpdateAsync(Restaurant restaurant);
        Task DeleteAsync(Restaurant restaurant);
    }
}
