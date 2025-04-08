﻿using AutoMapper;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;
using Restaurants.Application.Commands.Resraurant;
using Restaurants.Application.Users;
using Restaurants.Domain.IRepositories;
using Xunit;

namespace Restaurants.Application.Handlers.Restaurant.Tests
{
    public class CreateRestaurantCommandHandlerTests
    {
        [Fact()]
        public async Task Handle_ForValidCommand_ReturnsCreatedRestaurantId()
        {
            // arrange
            var loggerMock = new Mock<ILogger<CreateRestaurantCommandHandler>>();

            var mapperMock = new Mock<IMapper>();

            var command = new CreateRestaurantCommand();

            var restaurant = new Restaurants.Domain.Entities.Restaurant();

            mapperMock.Setup(m => m.Map<Restaurants.Domain.Entities.Restaurant>(command)).Returns(restaurant);

            var restaurantRepositoryMock = new Mock<IRestaurantsRepository>();

            restaurantRepositoryMock.Setup(repo => repo.CreateAsync(It.IsAny<Restaurants.Domain.Entities.Restaurant>())).ReturnsAsync(1);

            var userContextMock = new Mock<IUserContext>();

            var currentUser = new CurrentUser("owner-id", "test@test.com", [], null, null);

            userContextMock.Setup(u => u.GetCurrentUser()).Returns(currentUser);

            var commandHandler = new CreateRestaurantCommandHandler(loggerMock.Object, mapperMock.Object, restaurantRepositoryMock.Object, userContextMock.Object);

            // act
            var result = await commandHandler.Handle(command, CancellationToken.None);

            // assert
            result.Should().Be(1);
            restaurant.OwnerId.Should().Be("owner-id");
            restaurantRepositoryMock.Verify(r => r.CreateAsync(restaurant), Times.Once);
        }
    }
}