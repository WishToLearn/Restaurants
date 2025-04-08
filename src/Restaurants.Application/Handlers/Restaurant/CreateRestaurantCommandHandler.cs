using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Application.Commands.Resraurant;
using Restaurants.Application.Users;
using Restaurants.Domain.IRepositories;

namespace Restaurants.Application.Handlers.Restaurant
{
    public class CreateRestaurantCommandHandler(ILogger<CreateRestaurantCommandHandler> logger, IMapper mapper, IRestaurantsRepository restaurantsRepository, IUserContext userContext) : IRequestHandler<CreateRestaurantCommand, int>
    {
        public async Task<int> Handle(CreateRestaurantCommand request, CancellationToken cancellationToken)
        {
            var currentUser = userContext.GetCurrentUser();

            logger.LogInformation("{UserEmail} - {UserId} is creating a {@Restaurant}", currentUser!.Email, currentUser.Id, request);

            var createRestaurant = mapper.Map<Restaurants.Domain.Entities.Restaurant>(request);

            createRestaurant.OwnerId = currentUser.Id;

            return await restaurantsRepository.CreateAsync(createRestaurant);
        }
    }
}
