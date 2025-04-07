using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Restaurants.Domain.Constants;
using Restaurants.Domain.Entities;
using Restaurants.Infrastructure.Persistance;

namespace Restaurants.Infrastructure.Seeders
{
    internal class RestaurantSeeder(RestaurantsDbContext dbContext) : IRestaurantSeeder
    {
        public async Task Seed()
        {
            if(dbContext.Database.GetPendingMigrations().Any())
            {
                await dbContext.Database.MigrateAsync();
            }

            if (await dbContext.Database.CanConnectAsync())
            {
                if (!dbContext.Restaurants.Any())
                {
                    var restaurants = GetRestaurants();
                    dbContext.Restaurants.AddRange(restaurants);
                    await dbContext.SaveChangesAsync();
                }

                if (!dbContext.Roles.Any())
                {
                    var roles = GetRoles();
                    dbContext.Roles.AddRange(roles);
                    await dbContext.SaveChangesAsync();
                }
            }
        }

        private IEnumerable<Restaurant> GetRestaurants()
        {
            var owner = new User
            {
                Email = "seed-user@test.com"
            };

            var restaurants = new List<Restaurant>
            {
                new Restaurant
                {
                    Name = "Spice Villa",
                    Description = "Authentic Indian flavors.",
                    Category = "Indian",
                    HasDelivery = true,
                    ContactEmail = "contact@spicevilla.com",
                    ContactNumber = "111-222-3333",
                    Owner = owner,
                    Address = new Address { City = "New York", Street = "Lexington Ave", PostalCode = "10001" },
                    Dishes = new List<Dish>
                    {
                        new Dish { Name = "Butter Chicken", Description = "Creamy tomato-based chicken curry", Price = 15.99m, KiloCalories = 450 },
                        new Dish { Name = "Paneer Tikka", Description = "Grilled cottage cheese cubes", Price = 12.50m, KiloCalories = 300 }
                    }
                },
                new Restaurant
                {
                    Name = "Taco Haven",
                    Description = "Authentic Mexican tacos.",
                    Category = "Mexican",
                    HasDelivery = false,
                    ContactEmail = "info@tacohaven.com",
                    ContactNumber = "987-654-3210",
                    Owner = owner,
                    Address = new Address { City = "Los Angeles", Street = "Sunset Blvd", PostalCode = "90028" },
                    Dishes = new List<Dish>
                    {
                        new Dish { Name = "Chicken Tacos", Description = "Soft tacos with grilled chicken", Price = 10.99m, KiloCalories = 350 },
                        new Dish { Name = "Veggie Quesadilla", Description = "Grilled tortilla with cheese and vegetables", Price = 9.50m, KiloCalories = 320 }
                    }
                },
                new Restaurant
                {
                    Name = "Pasta Paradise",
                    Description = "Delicious Italian pasta dishes.",
                    Category = "Italian",
                    HasDelivery = false,
                    ContactEmail = "hello@pastaparadise.com",
                    ContactNumber = "222-888-9999",
                    Owner = owner,
                    Address = new Address { City = "Miami", Street = "Ocean Drive", PostalCode = "33139" },
                    Dishes = new List<Dish>
                    {
                        new Dish { Name = "Spaghetti Carbonara", Description = "Classic carbonara pasta", Price = 14.99m, KiloCalories = 450 },
                        new Dish { Name = "Margherita Pizza", Description = "Classic Italian pizza with mozzarella and basil", Price = 12.99m, KiloCalories = 600 }
                    }
                },
                new Restaurant
                {
                    Name = "American Diner",
                    Description = "Classic American comfort food.",
                    Category = "American",
                    HasDelivery = true,
                    ContactEmail = "info@americandiner.com",
                    ContactNumber = "444-321-6789",
                    Owner = owner,
                    Address = new Address { City = "Chicago", Street = "Lake Shore Drive", PostalCode = "60601" },
                    Dishes = new List<Dish>
                    {
                        new Dish { Name = "Cheese Burger", Description = "Classic cheeseburger with fries", Price = 8.99m, KiloCalories = 700 },
                        new Dish { Name = "Grilled Chicken Sandwich", Description = "Grilled chicken with fresh veggies", Price = 9.50m, KiloCalories = 550 }
                    }
                },
                new Restaurant
                {
                    Name = "Saffron Delight",
                    Description = "Traditional Afghan cuisine.",
                    Category = "Afghan",
                    HasDelivery = true,
                    ContactEmail = "support@saffrondelight.com",
                    ContactNumber = "333-777-5555",
                    Owner = owner,
                    Address = new Address { City = "Dallas", Street = "Main Street", PostalCode = "75201" },
                    Dishes = new List<Dish>
                    {
                        new Dish { Name = "Kabuli Pulao", Description = "Rice with lamb and raisins", Price = 13.99m, KiloCalories = 650 },
                        new Dish { Name = "Bolani", Description = "Flatbread stuffed with potatoes and herbs", Price = 7.99m, KiloCalories = 300 }
                    }
                },
                new Restaurant
                {
                    Name = "Little Italy",
                    Description = "Authentic Italian delicacies.",
                    Category = "Italian",
                    HasDelivery = true,
                    ContactEmail = "info@littleitaly.com",
                    ContactNumber = "666-123-4567",
                    Owner = owner,
                    Address = new Address { City = "San Francisco", Street = "Fisherman's Wharf", PostalCode = "94109" },
                    Dishes = new List<Dish>
                    {
                        new Dish { Name = "Penne Arrabbiata", Description = "Spicy tomato pasta", Price = 13.50m, KiloCalories = 400 },
                        new Dish { Name = "Tiramisu", Description = "Classic Italian dessert", Price = 6.99m, KiloCalories = 450 }
                    }
                },
                new Restaurant
                {
                    Name = "Mexican Fiesta",
                    Description = "Street-style Mexican food.",
                    Category = "Mexican",
                    HasDelivery = false,
                    ContactEmail = "contact@mexicanfiesta.com",
                    ContactNumber = "999-333-2222",
                    Owner = owner,
                    Address = new Address { City = "San Diego", Street = "Gaslamp Quarter", PostalCode = "92101" },
                    Dishes = new List<Dish>
                    {
                        new Dish { Name = "Enchiladas", Description = "Tortillas filled with chicken and cheese", Price = 11.99m, KiloCalories = 500 },
                        new Dish { Name = "Nachos Supreme", Description = "Crispy nachos with salsa and guacamole", Price = 8.99m, KiloCalories = 450 }
                    }
                },
                new Restaurant
                {
                    Name = "Biryani House",
                    Description = "Authentic Indian Biryani.",
                    Category = "Indian",
                    HasDelivery = true,
                    ContactEmail = "biryani@biryanihouse.com",
                    ContactNumber = "555-666-7777",
                    Owner = owner,
                    Address = new Address { City = "Houston", Street = "Richmond Ave", PostalCode = "77057" },
                    Dishes = new List<Dish>
                    {
                        new Dish { Name = "Hyderabadi Biryani", Description = "Basmati rice cooked with spices and chicken", Price = 14.99m, KiloCalories = 700 },
                        new Dish { Name = "Gulab Jamun", Description = "Sweet milk-based dessert", Price = 5.50m, KiloCalories = 350 }
                    }
                },
                new Restaurant
                {
                    Name = "Sunrise Bakery",
                    Description = "Freshly baked goods every day.",
                    Category = "Bakery",
                    HasDelivery = true,
                    ContactEmail = "order@sunrisebakery.com",
                    ContactNumber = "777-888-9999",
                    Owner = owner,
                    Address = new Address { City = "Seattle", Street = "Pine Street", PostalCode = "98101" },
                    Dishes = new List<Dish>
                    {
                        new Dish { Name = "Chocolate Croissant", Description = "Buttery croissant filled with chocolate", Price = 3.99m, KiloCalories = 350 },
                        new Dish { Name = "Blueberry Muffin", Description = "Moist muffin with fresh blueberries", Price = 2.99m, KiloCalories = 300 }
                    }
                }
            };

            return restaurants;
        }

        private IEnumerable<IdentityRole> GetRoles()
        {
            var roles = new List<IdentityRole>
            {
                new IdentityRole
                {
                    Name = nameof(UserRoles.User),
                    NormalizedName = nameof(UserRoles.User).ToUpper()
                },
                new IdentityRole
                {
                    Name = nameof(UserRoles.Owner),
                    NormalizedName= nameof(UserRoles.Owner).ToUpper()
                },
                new IdentityRole
                {
                    Name = nameof(UserRoles.Admin),
                    NormalizedName = nameof(UserRoles.Admin).ToUpper()
                }
            };

            return roles;
        }
    }
}
