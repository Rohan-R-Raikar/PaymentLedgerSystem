using IdentityService.Models;
using Microsoft.EntityFrameworkCore;

namespace IdentityService.Data
{
    public class IdentityDbSeeder
    {
        public static async Task SeedAsync(IdentityDbContext context)
        {
            await context.Database.MigrateAsync();
            if (!context.Roles.Any())
            {
                var adminRole = new Role { Id = Guid.NewGuid(), Name = "Admin" };
                var userRole = new Role { Id = Guid.NewGuid(), Name = "User" };

                context.Roles.AddRange(adminRole, userRole);
                await context.SaveChangesAsync();
            }

            if (!context.Permissions.Any())
            {
                var permissions = new[]
                {
                    new Permission { Id = Guid.NewGuid(), Name = "User.Read" },
                    new Permission { Id = Guid.NewGuid(), Name = "User.Create" },
                    new Permission { Id = Guid.NewGuid(), Name = "User.Update" },
                    new Permission { Id = Guid.NewGuid(), Name = "User.Delete" }
                };

                context.Permissions.AddRange(permissions);
                await context.SaveChangesAsync();
            }

            if (!context.RolePermissions.Any())
            {
                var adminRole = await context.Roles.FirstAsync(r => r.Name == "Admin");
                var userRole = await context.Roles.FirstAsync(r => r.Name == "User");

                var permissions = await context.Permissions.ToListAsync();

                var mappings = new List<RolePermission>();

                mappings.AddRange(
                    permissions.Select(p => new RolePermission
                    {
                        RoleId = adminRole.Id,
                        PermissionId = p.Id
                    })
                );

                var readPermission = permissions.First(p => p.Name == "User.Read");

                mappings.Add(new RolePermission
                {
                    RoleId = userRole.Id,
                    PermissionId = readPermission.Id
                });

                context.RolePermissions.AddRange(mappings);
                await context.SaveChangesAsync();
            }
        }
    }
}
