using Contracts;

namespace Emag.ExtensionMethods
{
    internal static class ServiceCollectionExtensionMethods
    {
        internal static IServiceCollection AddCurrentUser(this IServiceCollection services)
        {
            services.AddScoped(s =>
            {
                var accessor = s.GetService<IHttpContextAccessor>();
                var httpContext = accessor.HttpContext;
                var claims = httpContext.User.Claims;

                var userIdClaim = claims?.FirstOrDefault(c => c.Type == "Id")?.Value;
                var isParsingSuccessful = Guid.TryParse(userIdClaim, out Guid id);

                return new CurrentUserDTO
                {
                    Id = id,
                    RoleId = int.Parse(claims?.FirstOrDefault(c => c.Type == "RoleId")?.Value ?? "1"),
                };
            });

            return services;
        }
    }
}
