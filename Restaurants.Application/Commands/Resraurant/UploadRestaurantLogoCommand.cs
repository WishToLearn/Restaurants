using MediatR;

namespace Restaurants.Application.Commands.Resraurant
{
    public class UploadRestaurantLogoCommand : IRequest
    {
        public int RestaurantId { get; set; }
        public string FileName { get; set; } = default!;
        public Stream File { get; set; } = default!;
    }
}
