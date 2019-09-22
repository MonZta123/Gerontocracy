using System.Threading.Tasks;

using Gerontocracy.Data.Entities.Account;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace Gerontocracy.Core.Config
{
    public static class SeedExtensions
    {
        #region Methods

        public static IApplicationBuilder EnsureSeed(this IApplicationBuilder app)
        {
            using (var scope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                var userManager = scope.ServiceProvider.GetRequiredService<UserManager<User>>();
                var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<Role>>();
                var settings = scope.ServiceProvider.GetRequiredService<GerontocracySettings>();

                EnsureSeedRoles(roleManager).Wait();
                EnsureSeedUser(userManager, settings).Wait();
                return app;
            }
        }

        public static async Task EnsureSeedRoles(RoleManager<Role> roleManager)
        {
            if (await roleManager.FindByNameAsync("admin") == null)
                await roleManager.CreateAsync(new Role { Name = "admin" });

            if (await roleManager.FindByNameAsync("moderator") == null)
                await roleManager.CreateAsync(new Role { Name = "moderator" });
        }

        public static async Task EnsureSeedUser(UserManager<User> userManager, GerontocracySettings config)
        {
            if (userManager.FindByEmailAsync(config.AdminEmail).Result == null)
            {
                var user = new User
                {
                    UserName = config.AdminUser,
                    Email = config.AdminEmail,
                    EmailConfirmed = true
                };

                var result = await userManager.CreateAsync(user, config.AdminPassword);

                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(user, "admin");
                }
            }
        }

        #endregion Methods
    }
}
