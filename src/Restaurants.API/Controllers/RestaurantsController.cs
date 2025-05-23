﻿using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Restaurants.Application.Commands.Resraurant;
using Restaurants.Application.DTOs;
using Restaurants.Application.Queries.Restaurant;
using Restaurants.Domain.Constants;

namespace Restaurants.API.Controllers
{
    [ApiController]
    [Route("api/restaurants")]
    [Authorize]
    public class RestaurantsController(IMediator mediator) : ControllerBase
    {
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<RestaurantDTO>>> GetAllRestaurants([FromQuery] GetAllRestaurantsQuery request)
        {
            var restaurants = await mediator.Send(request);

            if (!restaurants.Items.Any())
            {
                return NoContent();
            }

            return Ok(restaurants);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [AllowAnonymous]
        public async Task<ActionResult<RestaurantDTO>> GetRestaurant([FromRoute]int id)
        {
            var restaurant = await mediator.Send(new GetRestaurantsByIdQuery(id));

            return Ok(restaurant);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [Authorize(Roles = nameof(UserRoles.Owner))]
        public async Task<IActionResult> CreateRestaurant([FromBody]CreateRestaurantCommand request)
        {
            var id = await mediator.Send(request);

            return CreatedAtAction(nameof(GetRestaurant), new { id }, null);
        }

        [HttpPatch("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateRestaurant([FromRoute]int id, [FromBody]UpdateRestaurantCommand request)
        {
            request.Id = id;

            await mediator.Send(request);

            return NoContent();
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteRestaurant([FromRoute] int id)
        {
            await mediator.Send(new DeleteRestaurantCommand(id));

            return NoContent();
        }

        [HttpPost("{id}/logo")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Authorize(Roles = nameof(UserRoles.Owner))]
        public async Task<IActionResult> UploadLogo([FromRoute] int id, IFormFile file)
        {
            using var stream = file.OpenReadStream();

            var request = new UploadRestaurantLogoCommand()
            {
                RestaurantId = id,
                FileName = file.FileName,
                File = stream
            };

            await mediator.Send(request);

            return NoContent();
        }
    }
}
