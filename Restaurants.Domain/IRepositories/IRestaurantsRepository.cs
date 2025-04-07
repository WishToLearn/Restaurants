using Restaurants.Domain.Common;
using Restaurants.Domain.Entities;

namespace Restaurants.Domain.IRepositories
{
    public interface IRestaurantsRepository
    {
        Task<int> CreateAsync(Restaurant restaurant);
        Task<(IEnumerable<Restaurant>, int)> GetAllAsync(string? searchPhrase, int pageNumber, int pageSize, string? sortBy, SortDirection sortDirection);
        Task<Restaurant?> GetAsync(int id);
        Task UpdateAsync(Restaurant restaurant);
        Task DeleteAsync(Restaurant restaurant);
    }
}
