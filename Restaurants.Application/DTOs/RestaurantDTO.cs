using Restaurants.Domain.Entities;

namespace Restaurants.Application.DTOs
{
    public class RestaurantDTO
    {
        public int Id { get; set; }
        public string Name { get; set; } = default!;
        public string Description { get; set; } = default!;
        public string Category { get; set; } = default!;
        public bool HasDelivery { get; set; }
        public string? City { get; set; }
        public string? Street { get; set; }
        public string? PostalCode { get; set; }
        public string? LogoSasUrl { get; set; }
        public List<Dish> Dishes { get; set; } = default!;
    }
}
