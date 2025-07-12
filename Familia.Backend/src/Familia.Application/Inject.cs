﻿using Familia.Application.Volunteers.CreateVolunteer;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace Familia.Application
{
    public static class Inject
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddScoped<CreateVolunteerHandler>();

            services.AddValidatorsFromAssembly(typeof(Inject).Assembly);

            return services;
        }
    }
}
