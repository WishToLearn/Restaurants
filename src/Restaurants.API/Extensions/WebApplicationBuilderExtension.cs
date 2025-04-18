﻿using Microsoft.Extensions.Configuration;
using Microsoft.OpenApi.Models;
using Restaurants.API.Middlewares;
using Serilog;

namespace Restaurants.API.Extensions
{
    public static class WebApplicationBuilderExtension
    {
        public static void AddPresentation(this WebApplicationBuilder builder)
        {
            builder.Services.AddCors(options =>
            {
                /*
                options.AddPolicy("AllowLocalhost", policy =>
                {
                    policy.WithOrigins("http://localhost:3000")
                          .AllowAnyMethod()
                          .AllowAnyHeader();
                });

                options.AddPolicy("AllowRestricted", policy =>
                {
                    policy.WithOrigins(
                                "https://app1.example.com",
                                "https://app2.example.com"
                            )
                          .WithMethods("GET", "POST")
                          .WithHeaders("Content-Type", "Authorization");
                });
                */

                options.AddPolicy("AllowAll", policy =>
                {
                    policy.AllowAnyOrigin()
                          .AllowAnyMethod()
                          .AllowAnyHeader();
                });
            });

            builder.Services.AddControllers();

            builder.Services.AddScoped<ErrorHandlingMiddleware>();

            builder.Services.AddScoped<RequestTimeLoggingMiddleware>();

            builder.Host.UseSerilog((context, configuration) =>
            {
                configuration.ReadFrom.Configuration(context.Configuration);
            });

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
