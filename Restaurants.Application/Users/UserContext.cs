﻿using System.Security.Claims;
using Microsoft.AspNetCore.Http;

namespace Restaurants.Application.Users
{
    public interface IUserContext
    {
        CurrentUser? GetCurrentUser();
    }

    public class UserContext(IHttpContextAccessor httpContextAccessor) : IUserContext
    {
        public CurrentUser? GetCurrentUser()
        {
            var user = httpContextAccessor?.HttpContext?.User;

            if (user is null)
            {
                throw new InvalidOperationException("User context is not present");
            }

            if (user.Identity is null || !user.Identity.IsAuthenticated)
            {
                return null;
            }

            var userId = user.FindFirst(claim => claim.Type == ClaimTypes.NameIdentifier)!.Value;
            var email = user.FindFirst(claim => claim.Type == ClaimTypes.Email)!.Value;
            var roles = user.Claims.Where(claim => claim.Type == ClaimTypes.Role)!.Select(claim => claim.Value);
            var nationality = user.FindFirst(claim => claim.Type == "Nationality")?.Value;
            var dobString = user.FindFirst(claim => claim.Type == "DateOfBirth")?.Value;
            var dob = dobString is null ? (DateOnly?)null : DateOnly.ParseExact(dobString, "yyyy-MM-dd");
            return new CurrentUser(userId, email, roles, nationality, dob);
        }
    }
}
