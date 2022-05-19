using System;
using System.Data.Entity.Migrations;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using VideoClub.Data.Context;
using VideoClub.Data.Entities;

namespace VideoClub.Data.Migrations
{
    public class Configuration : DbMigrationsConfiguration<ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(ApplicationDbContext context)
        {
            var adminRole = context.Roles.Add(new IdentityRole { Name = "Admin" });

            var hasher = new PasswordHasher();
            var adminUser = context.Users.Add(
                new User
                {
                    FirstName = "Default",
                    LastName = "Default",
                    UserName = "default@default.com",
                    Email = "default@default.com",
                    PasswordHash = hasher.HashPassword("Pa$$w0rd"),
                    SecurityStamp = Guid.NewGuid().ToString("D")
                }
            );

            adminUser.Roles.Add(new IdentityUserRole
            {
                UserId = adminUser.Id,
                RoleId = adminRole.Id
            });

            base.Seed(context);
        }
    }
}