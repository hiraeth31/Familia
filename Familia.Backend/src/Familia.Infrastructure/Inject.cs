using Familia.Application.Volunteers;
using Familia.Infrastructure.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace Familia.Infrastructure
{
    public static class Inject
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            services.AddScoped<ApplicationDbContext>();
            services.AddScoped<IVolunteersRepository, VolunteersRepository>();

            return services;
        }
    }
}
