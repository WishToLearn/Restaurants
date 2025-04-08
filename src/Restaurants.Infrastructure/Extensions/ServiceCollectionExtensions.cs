using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Interfaces;
using Restaurants.Domain.IRepositories;
using Restaurants.Infrastructure.Authorization;
using Restaurants.Infrastructure.Authorization.Services;
using Restaurants.Infrastructure.Configurations;
using Restaurants.Infrastructure.Persistance;
using Restaurants.Infrastructure.Repositories;
using Restaurants.Infrastructure.Seeders;
using Restaurants.Infrastructure.Storage;

namespace Restaurants.Infrastructure.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("RestaurantDB");

            services.AddDbContext<RestaurantsDbContext>(options => options.UseSqlServer(connectionString));

            services.AddIdentityApiEndpoints<User>()
                .AddRoles<IdentityRole>()
                .AddClaimsPrincipalFactory<RestaurantUserClaimsPrincipalFactory>()
                .AddEntityFrameworkStores<RestaurantsDbContext>();

            services.AddScoped<IRestaurantSeeder, RestaurantSeeder>();

            services.AddScoped<IRestaurantsRepository, RestaurantsRepository>();

            services.AddScoped<IDishesRepository, DishesRespository>();

            services.AddAuthorizationBuilder()
                .AddPolicy(PolicyName.HasNationality, builder => builder.RequireClaim(ApplicationClaimTypes.Nationality, "Indian", "American", "Polish"))
                .AddPolicy(PolicyName.AtLeast21, builder => builder.AddRequirements(new MinimunAgeRequirement(21)));

            services.AddScoped<IAuthorizationHandler, MinimunAgeRequirementHandler>();

            services.AddScoped<IRestaurantAuthorizationService, RestaurantAuthorizationService>();

            services.Configure<BlobStorageSettings>(configuration.GetSection("BlobStorage"));

            services.AddScoped<IBlobStorageService, BlobStorageService>();
        }
    }
}
