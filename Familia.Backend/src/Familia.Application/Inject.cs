using Familia.Application.Volunteers.CreateVolunteer;
using Microsoft.Extensions.DependencyInjection;

namespace Familia.Application
{
    public static class Inject
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddScoped<CreateVolunteerHandler>();

            return services;
        }
    }
}
