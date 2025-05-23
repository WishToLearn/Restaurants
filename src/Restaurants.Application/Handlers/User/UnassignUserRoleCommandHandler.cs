﻿using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Restaurants.Application.Commands.User;
using Restaurants.Domain.Exceptions;

namespace Restaurants.Application.Handlers.User
{
    public class UnassignUserRoleCommandHandler(ILogger<UnassignUserRoleCommandHandler> logger, UserManager<Restaurants.Domain.Entities.User> userManager, RoleManager<IdentityRole> roleManager) : IRequestHandler<UnassignUserRoleCommand>
    {
        public async Task Handle(UnassignUserRoleCommand request, CancellationToken cancellationToken)
        {
            logger.LogInformation("Unassigning role to a user");

            var user = await userManager.FindByEmailAsync(request.UserEmail);

            if (user is null)
            {
                throw new NotFoundException(nameof(Restaurants.Domain.Entities.User), request.UserEmail);
            }

            var role = await roleManager.FindByNameAsync(request.RoleName);

            if (role is null)
            {
                throw new NotFoundException(nameof(IdentityRole), request.RoleName);
            }

            await userManager.RemoveFromRoleAsync(user, role.Name!);
        }
    }
}
