using Microsoft.EntityFrameworkCore;

namespace UserService.Data
{
    public class DbInitializer
    {
        public static void InitDb(WebApplication app)
        {
            using var scope = app.Services.CreateScope();
            SeedData(scope.ServiceProvider.GetService<UserDbContext>());
        }

        private static void SeedData(UserDbContext context)
        {
            context.Database.Migrate();
            context.SaveChanges();
        }
    }
}
