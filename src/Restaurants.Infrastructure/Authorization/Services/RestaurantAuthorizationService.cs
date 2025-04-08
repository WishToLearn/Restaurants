using Microsoft.Extensions.Logging;
using Restaurants.Application.Users;
using Restaurants.Domain.Constants;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Interfaces;

namespace Restaurants.Infrastructure.Authorization.Services
{
    internal class RestaurantAuthorizationService(ILogger<RestaurantAuthorizationService> logger, IUserContext userContext) : IRestaurantAuthorizationService
    {
        public bool IsAuthorize(Restaurant restaurant, ResourceOperation resourceOperation)
        {
            var currentUser = userContext.GetCurrentUser();

            logger.LogInformation("Authorizing user {UserEmail} to {ResourceOperation} for {Restaurant}", currentUser!.Email, resourceOperation, restaurant.Name);

            if(resourceOperation == ResourceOperation.Create || resourceOperation == ResourceOperation.Read)
            {
                logger.LogInformation("Create or Read Operation - Authorization Successful");

                return true;
            }

            if (resourceOperation == ResourceOperation.Delete && currentUser.IsInRole(nameof(UserRoles.Owner)))
            {
                logger.LogInformation("Admin, Delete Operation - Authorization Successful");

                return true;
            }

            if ((resourceOperation == ResourceOperation.Update || resourceOperation == ResourceOperation.Delete) && currentUser.Id == restaurant.OwnerId)
            {
                logger.LogInformation("Restaurant Owner, Update or Delete Operation - Authorization Successful");

                return true;
            }

            return false;
        }
    }
}
