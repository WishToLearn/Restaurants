using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;
using Restaurants.Application.Users;

namespace Restaurants.Infrastructure.Authorization
{
    internal class MinimunAgeRequirementHandler(ILogger<MinimunAgeRequirementHandler> logger, IUserContext userContext) : AuthorizationHandler<MinimunAgeRequirement>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, MinimunAgeRequirement requirement)
        {
            var currentUser =  userContext.GetCurrentUser();

            logger.LogInformation("User: {Email}, Date of Birth {DOB} - Handling Minimum Age Requirement", currentUser!.Email, currentUser.DateOfBirth);

            if (currentUser.DateOfBirth is null)
            {
                logger.LogInformation("User DOB is Null");

                context.Fail();

                return Task.CompletedTask;
            }

            if (currentUser.DateOfBirth.Value.AddYears(requirement.MinimumAge) <= DateOnly.FromDateTime(DateTime.Today))
            {
                logger.LogInformation("Authorization succeeded");

                context.Succeed(requirement);
            }
            else
            {
                context.Fail();
            }

            return Task.CompletedTask;
        }
    }
}
