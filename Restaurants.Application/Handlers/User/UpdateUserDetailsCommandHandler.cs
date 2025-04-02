using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Restaurants.Application.Commands.User;
using Restaurants.Application.Users;
using Restaurants.Domain.Exceptions;

namespace Restaurants.Application.Handlers.User
{
    public class UpdateUserDetailsCommandHandler(ILogger<UpdateUserDetailsCommandHandler> logger, IUserContext userContext, IUserStore<Restaurants.Domain.Entities.User> userStore) : IRequestHandler<UpdateUserDetailsCommand>
    { 
        public async Task Handle(UpdateUserDetailsCommand request, CancellationToken cancellationToken)
        {
            var currentUser = userContext.GetCurrentUser();

            logger.LogInformation($"Updating a user with userId: {currentUser!.Id} with following request {request}");

            var dbUser = await userStore.FindByIdAsync(currentUser!.Id, cancellationToken);

            if (dbUser == null)
            {
                throw new NotFoundException(nameof(Restaurants.Domain.Entities.User), currentUser!.Id);
            }

            dbUser.DateOfBirth = request.DateOfBirth;
            dbUser.Nationality = request.Nationality;

            await userStore.UpdateAsync(dbUser, cancellationToken);
        }
    }
}
