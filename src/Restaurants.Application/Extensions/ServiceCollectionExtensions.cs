﻿using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.Extensions.DependencyInjection;
using Restaurants.Application.Users;

namespace Restaurants.Application.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddApplication(this IServiceCollection services)
        {
            var applicationAssembly = typeof(ServiceCollectionExtensions).Assembly;

            services.AddMediatR(configuration => configuration.RegisterServicesFromAssemblies(applicationAssembly));
            
            services.AddAutoMapper(applicationAssembly);
            
            services.AddValidatorsFromAssembly(applicationAssembly).AddFluentValidationAutoValidation();

            services.AddScoped<IUserContext, UserContext>();
            services.AddHttpContextAccessor();
        }
    }
}
