using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using PartsCatalog.Data;
using PartsCatalog.Data.Models;

namespace PartsCatalog.Infrastructure.Extensions
{
    public static class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder UseDatabaseMigration(this IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                serviceScope.ServiceProvider.GetService<PartsCatalogDbContext>().Database.Migrate();

                var userManager = serviceScope.ServiceProvider.GetService<UserManager<User>>();
                var roleManager = serviceScope.ServiceProvider.GetService<RoleManager<IdentityRole>>();

                Task
                    .Run(async () =>
                    {
                       

                        var role = new IdentityRole("Administrator");

                             await roleManager.CreateAsync(role);

                        
                        
                        
                        var adminEmail = "admin@admin.com";

                        var adminUser = await userManager.FindByEmailAsync(adminEmail);

                        if (adminUser == null)
                        {
                            adminUser = new User
                            {
                                Email = adminEmail,
                                UserName = adminEmail,
                            };

                            var roleName = "Administrator";
                           
                            
                            await userManager.CreateAsync(adminUser, "123456");

                            await userManager.AddToRoleAsync(adminUser, roleName);
                        }
                    })
                    .Wait();
            }

            return app;
        }
    }
}
