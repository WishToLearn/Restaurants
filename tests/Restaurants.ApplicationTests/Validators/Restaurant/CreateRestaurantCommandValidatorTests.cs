using FluentValidation.TestHelper;
using Restaurants.Application.Commands.Resraurant;
using Xunit;

namespace Restaurants.Application.Validators.Restaurant.Tests
{
    public class CreateRestaurantCommandValidatorTests
    {
        [Fact()]
        public void Validator_ForValidCommand_ShouldNotHaveValidationErrors()
        {
            // arrange
            var command = new CreateRestaurantCommand()
            {
                Name = "Test Name",
                Description = "Test Description",
                Category = "Indian",
                ContactEmail = "test@test.com",
                PostalCode = "12-345",
            };

            var validator = new CreateRestaurantCommandValidator();

            // act
            var result = validator.TestValidate(command);

            // assert
            result.ShouldNotHaveAnyValidationErrors();
        }

        [Fact()]
        public void Validator_ForInvalidCommand_ShouldHaveValidationErrors()
        {
            // arrange
            var command = new CreateRestaurantCommand()
            {
                Name = "TR",
                Category = "Italy",
                ContactEmail = "@test.com",
                PostalCode = "12345",
            };

            var validator = new CreateRestaurantCommandValidator();

            // act
            var result = validator.TestValidate(command);

            // assert
            result.ShouldHaveValidationErrorFor(c => c.Name);
            result.ShouldHaveValidationErrorFor(c => c.Category);
            result.ShouldHaveValidationErrorFor(c => c.ContactEmail);
            result.ShouldHaveValidationErrorFor(c => c.PostalCode);
        }
        
        [Theory()]
        [InlineData("Indian")]
        [InlineData("Mexican")]
        [InlineData("American")]
        [InlineData("Italian")]
        [InlineData("Afghan")]
        [InlineData("Chinese")]
        public void Validator_ForValidCategory_ShouldNotHaveValidationErrorsForCategoryProperty(string category)
        {
            // arrange
            var validator = new CreateRestaurantCommandValidator();
            var command = new CreateRestaurantCommand { Category = category };

            // act
            var result = validator.TestValidate(command);

            // assert
            result.ShouldNotHaveValidationErrorFor(c => c.Category);
        }

        [Theory()]
        [InlineData("12345")]
        [InlineData("123-45")]
        [InlineData("12 345")]
        [InlineData("12-3 45")]
        public void Validator_ForInvalidPostalCode_ShouldHaveValidationErrorsForPostalCodeProperty(string postalCode)
        {
            // arrange
            var validator = new CreateRestaurantCommandValidator();
            var command = new CreateRestaurantCommand { PostalCode = postalCode };

            // act
            var result = validator.TestValidate(command);

            // assert
            result.ShouldHaveValidationErrorFor(c => c.PostalCode);
        }
    }
}