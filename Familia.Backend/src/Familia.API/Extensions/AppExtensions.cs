using Familia.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace Familia.API.Extensions
{
    public static class AppExtensions
    {
        public static async Task ApplyMigration(this WebApplication app)
        {
            await using var scope = app.Services.CreateAsyncScope();
            var dbcontext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

            await dbcontext.Database.MigrateAsync();
        }
    }
}
