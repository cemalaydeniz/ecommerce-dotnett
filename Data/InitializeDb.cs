using ecommerce_dotnet.Models;
using Microsoft.AspNetCore.Identity;

namespace ecommerce_dotnet.Data
{
    public class InitializeDb
    {
        private readonly IServiceProvider _serviceProvider;

        public InitializeDb(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public async Task<InitializeDb> InitializeRoles()
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
                var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<Role>>();

                var initialRoles = new[]
                {
                    new Role() { Name = "user" },
                    new Role() { Name = "admin" }
                    // More roles can be added here
                };

                foreach (var role in initialRoles)
                {
                    if (dbContext.Roles.FirstOrDefault(_ => _.Name == role.Name) == null)
                    {
                        await roleManager.CreateAsync(role);
                    }
                }
            }

            return this;
        }
    }
}
