using MediatR;
using Microsoft.AspNetCore.Mvc;
using Restaurants.Application.Commands.Dish;
using Restaurants.Application.DTOs;
using Restaurants.Application.Queries.Dish;

namespace Restaurants.API.Controllers
{
    [ApiController]
    [Route("api/restaurants/{restaurantId}/dishes")]
    public class DishesController(IMediator mediator) : ControllerBase
    {
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> CreateDish([FromRoute]int restaurantId, [FromBody]CreateDishCommand request)
        {
            request.RestaurantId = restaurantId;

            var dishId = await mediator.Send(request);

            return CreatedAtAction(nameof(CreateDish), new { restaurantId, dishId }, null);
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IEnumerable<DishDTO>>> GetAllDishesForRestaurant([FromRoute]int restaurantId)
        {
            var dishes = await mediator.Send(new GetAllDishesForRestaurantQuery(restaurantId));

            return Ok(dishes);
        }

        [HttpGet("{dishId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<DishDTO>> GetDishForRestaurant([FromRoute] int restaurantId, [FromRoute]int dishId)
        {
            var dish = await mediator.Send(new GetDishForRestaurantQuery(restaurantId, dishId));

            return Ok(dish);
        }

        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteAllDishesForRestaurant([FromRoute]int restaurantId)
        {
            await mediator.Send(new DeleteAllDishesForRestaurantCommand(restaurantId));

            return NoContent();
        }
    }
}
