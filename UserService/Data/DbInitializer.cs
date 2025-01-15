using Microsoft.EntityFrameworkCore;
using UserService.Entities;

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

            if (context.Users.Any())
            {
                return;
            }

            var user = new User()
            {
                Id = Guid.NewGuid(),
                Email = "admin@admin.ro",
                Password = "8c6976e5b5410415bde908bd4dee15dfb167a9c873fc4bb8a81f6f2ab448a918",
                Role = Role.Admin,
            };

            context.Users.Add(user);
            context.SaveChanges();
        }
    }
}
