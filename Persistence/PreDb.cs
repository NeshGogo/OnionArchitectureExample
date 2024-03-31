using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Persistence
{
    public static class PreDb
    {
        public static void PrePoupulation(IServiceProvider serviceProvider)
        {
            using var scope = serviceProvider.CreateScope();
            using var context = scope.ServiceProvider.GetService<RepositoryDbContext>();
            SeedData(context);
        }

        private static void SeedData(RepositoryDbContext context)
        {
            try
            {
                Console.WriteLine("--> Attempting to apply migrations...");
                context.Database.Migrate();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"--> Could not run migrations. Error: {ex.Message}");
            }
        }
    }
}
