using Restaurants.Domain.Constants;
using Restaurants.Domain.Entities;

namespace Restaurants.Domain.Interfaces
{
    public interface IRestaurantAuthorizationService
    {
        bool IsAuthorize(Restaurant restaurant, ResourceOperation resourceOperation);
    }
}
