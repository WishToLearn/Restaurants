using Xunit;
using AutoMapper;
using Restaurants.Application.Commands.Resraurant;
using Restaurants.Domain.Entities;
using Restaurants.Application.DTOs;
using FluentAssertions;

namespace Restaurants.Application.Mappings.Tests
{
    public class RestaurantProfileTests
    {
        private IMapper _mapper;

        public RestaurantProfileTests()
        {
            var configuration = new MapperConfiguration(config =>
            {
                config.AddProfile<RestaurantProfile>();
            });

            _mapper = configuration.CreateMapper();
        }

        [Fact()]
        public void CreateMap_ForCreateRestaurantCommandToRestaurant_MapsCorrectly()
        {
            // arrange
            var command = new CreateRestaurantCommand
            {
                Name = "Test Restaurant",
                Description = "Test Description",
                Category = "Test Category",
                HasDelivery = true,
                ContactEmail = "test@example.com",
                ContactNumber = "0123456789",
                City = "Test City",
                Street = "Test Street",
                PostalCode = "12-345"
            };

            // act
            var restaurant = _mapper.Map<Restaurant>(command);

            // assert 

            restaurant.Should().NotBeNull();
            restaurant.Name.Should().Be(command.Name);
            restaurant.Description.Should().Be(command.Description);
            restaurant.Category.Should().Be(command.Category);
            restaurant.HasDelivery.Should().Be(command.HasDelivery);
            restaurant.ContactEmail.Should().Be(command.ContactEmail);
            restaurant.ContactNumber.Should().Be(command.ContactNumber);
            restaurant.Address.Should().NotBeNull();
            restaurant.Address.City.Should().Be(command.City);
            restaurant.Address.Street.Should().Be(command.Street);
            restaurant.Address.PostalCode.Should().Be(command.PostalCode);
        }

        [Fact()]
        public void CreateMap_ForRestaurantToRestaurantDto_MapsCorrectly()
        {
            // arrange
            var restaurant = new Restaurant()
            {
                Id = 12345,
                Name = "Test Restaurant",
                Description = "Test Description",
                Category = "Test Category",
                HasDelivery = true,
                ContactEmail = "test@example.com",
                ContactNumber = "0123456789",
                Address = new Address
                {
                    City = "Test City",
                    Street = "Test Street",
                    PostalCode = "12-345"
                }
            };

            // act
            var restaurantDTO = _mapper.Map<RestaurantDTO>(restaurant);

            // assert 
            restaurantDTO.Should().NotBeNull();
            restaurantDTO.Id.Should().Be(restaurant.Id);
            restaurantDTO.Name.Should().Be(restaurant.Name);
            restaurantDTO.Description.Should().Be(restaurant.Description);
            restaurantDTO.Category.Should().Be(restaurant.Category);
            restaurantDTO.HasDelivery.Should().Be(restaurant.HasDelivery);
            restaurantDTO.City.Should().Be(restaurant.Address.City);
            restaurantDTO.Street.Should().Be(restaurant.Address.Street);
            restaurantDTO.PostalCode.Should().Be(restaurant.Address.PostalCode);
        }

        [Fact()]
        public void CreateMap_ForUpdateRestaurantCommandToRestaurant_MapsCorrectly()
        {
            // arrange
            var command = new UpdateRestaurantCommand
            {
                Id = 12345,
                Name = "Updated Restaurant",
                Description = "Updated Description",
                HasDelivery = false
            };

            // act
            var restaurant = _mapper.Map<Restaurant>(command);

            // assert 
            restaurant.Should().NotBeNull();
            restaurant.Id.Should().Be(command.Id);
            restaurant.Name.Should().Be(command.Name);
            restaurant.Description.Should().Be(command.Description);
            restaurant.HasDelivery.Should().Be(command.HasDelivery);
        }
    }
}