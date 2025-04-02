using Microsoft.OpenApi.Models;
using Restaurants.API.Middlewares;

namespace Restaurants.API.Extensions
{
    public static class WebApplicationBuilderExtension
    {
        public static void AddPresentation(this WebApplicationBuilder builder)
        {
            builder.Services.AddControllers();

            builder.Services.AddScoped<ErrorHandlingMiddleware>();

            builder.Services.AddSwaggerGen(config =>
            {
                // Swagger config to support Identity endpoints.
                config.AddSecurityDefinition("bearerAuth", new OpenApiSecurityScheme
                {
                    Type = SecuritySchemeType.Http,
                    Scheme = "Bearer"
                });
                config.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "bearerAuth" }
                        },
                        []
                    }
                });
            });

            // Swagger doesn't support minimal api approach used by Identity package.
            builder.Services.AddEndpointsApiExplorer();
        }
    }
}
