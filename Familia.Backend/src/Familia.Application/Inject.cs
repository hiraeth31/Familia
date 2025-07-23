using Familia.Application.Volunteers.CreateVolunteer;
using Familia.Application.Volunteers.Delete;
using Familia.Application.Volunteers.UpdateMainInfo;
using Familia.Application.Volunteers.UpdateRequisite;
using Familia.Application.Volunteers.UpdateSocialMedia;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace Familia.Application
{
    public static class Inject
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddScoped<CreateVolunteerHandler>();
            services.AddScoped<UpdateMainInfoHandler>();
            services.AddScoped<UpdateSocialMediaHandler>();
            services.AddScoped<UpdateHelpRequisiteHandler>();
            services.AddScoped<DeleteHardVolunteerHandler>();
            services.AddScoped<DeleteSoftVolunteerHandler>();

            services.AddValidatorsFromAssembly(typeof(Inject).Assembly);

            return services;
        }
    }
}
