using Xunit;
using AutoMapper;
using FluentAssertions;
using Moq;
using Restaurants.Application.Commands.Resraurant;
using Restaurants.Domain.Exceptions;
using Restaurants.Domain.Interfaces;
using Restaurants.Domain.IRepositories;
using Microsoft.Extensions.Logging;
using Restaurants.Domain.Constants;

namespace Restaurants.Application.Handlers.Restaurant.Tests
{
    public class UpdateRestaurantCommandHandlerTests
    {
        private readonly Mock<ILogger<UpdateRestaurantCommandHandler>> _loggerMock;
        private readonly Mock<IRestaurantsRepository> _restaurantsRepositoryMock;
        private readonly Mock<IMapper> _mapperMock;
        private readonly Mock<IRestaurantAuthorizationService> _restaurantAuthorizationServiceMock;

        private readonly UpdateRestaurantCommandHandler _handler;

        public UpdateRestaurantCommandHandlerTests()
        {
            _loggerMock = new Mock<ILogger<UpdateRestaurantCommandHandler>>();

            _restaurantsRepositoryMock = new Mock<IRestaurantsRepository>();

            _mapperMock = new Mock<IMapper>();

            _restaurantAuthorizationServiceMock = new Mock<IRestaurantAuthorizationService>();

            _handler = new UpdateRestaurantCommandHandler(_loggerMock.Object, _mapperMock.Object, _restaurantsRepositoryMock.Object, _restaurantAuthorizationServiceMock.Object);
        }

        [Fact()]
        public async Task Handle_WithValidRequest_ShouldUpdateRestaurant()
        {
            // arrange
            var restaurantId = 123;
            var command = new UpdateRestaurantCommand()
            {
                Id = restaurantId,
                Name = "New Test",
                Description = "New Description",
                HasDelivery = true,
            };

            var restaurant = new Restaurants.Domain.Entities.Restaurant()
            {
                Id = restaurantId,
                Name = "Test",
                Description = "Test",
            };

            _restaurantsRepositoryMock.Setup(r => r.GetAsync(restaurantId)).ReturnsAsync(restaurant);

            _restaurantAuthorizationServiceMock.Setup(m => m.IsAuthorize(restaurant, Domain.Constants.ResourceOperation.Update)).Returns(true);

            // act
            await _handler.Handle(command, CancellationToken.None);

            // assert
            _restaurantsRepositoryMock.Verify(r => r.UpdateAsync(restaurant), Times.Once);
            _mapperMock.Verify(m => m.Map(command, restaurant), Times.Once);
        }

        [Fact]
        public async Task Handle_WithNonExistingRestaurant_ShouldThrowNotFoundException()
        {
            // Arrange
            var restaurantId = 123;
            var request = new UpdateRestaurantCommand
            {
                Id = restaurantId
            };

            _restaurantsRepositoryMock.Setup(r => r.GetAsync(restaurantId)).ReturnsAsync((Restaurants.Domain.Entities.Restaurant?)null);

            // act
            Func<Task> act = async () => await _handler.Handle(request, CancellationToken.None);

            // assert
            await act.Should().ThrowAsync<NotFoundException>().WithMessage($"Restaurant with Id: {restaurantId} doesn't exist");
        }

        [Fact]
        public async Task Handle_WithUnauthorizedUser_ShouldThrowForbidException()
        {
            // arrange
            var restaurantId = 123;
            var request = new UpdateRestaurantCommand
            {
                Id = restaurantId
            };

            var existingRestaurant = new Restaurants.Domain.Entities.Restaurant
            {
                Id = restaurantId
            };

            _restaurantsRepositoryMock.Setup(r => r.GetAsync(restaurantId)).ReturnsAsync(existingRestaurant);

            _restaurantAuthorizationServiceMock.Setup(a => a.IsAuthorize(existingRestaurant, ResourceOperation.Update)).Returns(false);

            // act
            Func<Task> act = async () => await _handler.Handle(request, CancellationToken.None);

            // assert
            await act.Should().ThrowAsync<ForbidException>();
        }
    }
}