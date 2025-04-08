using Microsoft.AspNetCore.Authorization;

namespace Restaurants.Infrastructure.Authorization
{
    public class MinimunAgeRequirement(int minimumAge) : IAuthorizationRequirement
    {
        public int MinimumAge { get; } = minimumAge;
    }
}
