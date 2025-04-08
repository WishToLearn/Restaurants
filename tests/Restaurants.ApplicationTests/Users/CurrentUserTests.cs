using FluentAssertions;
using Restaurants.Domain.Constants;
using Xunit;

namespace Restaurants.Application.Users.Tests
{
    public class CurrentUserTests
    {
        [Theory()]
        [InlineData(nameof(UserRoles.Admin))]
        [InlineData(nameof(UserRoles.Owner))]
        [InlineData(nameof(UserRoles.User))]
        public void IsInRole_WithMatchingRole_ShouldReturnTrue(string roleName)
        {
            // arrange
            var currentUser = new CurrentUser("12345", "test@test.com", [nameof(UserRoles.Admin), nameof(UserRoles.Owner), nameof(UserRoles.User)], null, null);

            // act
            var isInRole = currentUser.IsInRole(roleName);

            // assert
            isInRole.Should().BeTrue();
        }


        [Fact()]
        public void IsInRole_WithNoMatchingRole_ShouldReturnFalse()
        {
            // arrange
            var currentUser = new CurrentUser("1", "test@test.com", [nameof(UserRoles.Admin), nameof(UserRoles.User)], null, null);

            // act
            var isInRole = currentUser.IsInRole(nameof(UserRoles.Owner));

            // assert
            isInRole.Should().BeFalse();
        }

        [Fact()]
        public void IsInRole_WithNoMatchingRoleCase_ShouldReturnFalse()
        {
            // arrange
            var currentUser = new CurrentUser("12345", "test@test.com", [nameof(UserRoles.Admin), nameof(UserRoles.User)], null, null);

            // act
            var isInRole = currentUser.IsInRole(nameof(UserRoles.Admin).ToLower());

            // assert
            isInRole.Should().BeFalse();
        }
    }
}