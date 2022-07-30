
using EmployeeCrud.Domain;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace EmployeeCrud.Data
{
    public static class SeedDataExtension
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Employee>().HasData(
                new Employee
                {
                    Id = 1,
                    FirstName = "james",
                    LastName = "miller",
                    Email = "james@gmail.com"
                },
                new Employee
                {
                    Id = 2,
                    FirstName = "stewart",
                    LastName = "coffey",
                    Email = "stewart@gmail.com"
                },
                new Employee
                {
                    Id = 3,
                    FirstName = "david",
                    LastName = "fooster",
                    Email = "david@gmail.com"
                }
            );

            // Seed Roles

            List<IdentityRole> roles = new List<IdentityRole>()
            {
                new IdentityRole { Name = "Admin", NormalizedName = "ADMIN" },
                new IdentityRole { Name = "User", NormalizedName = "USER" }
            };

            modelBuilder.Entity<IdentityRole>().HasData(roles);

            // -----------------------------------------------------------------------------

            // Seed Users

            var passwordHasher = new PasswordHasher<IdentityUser>();

            List<IdentityUser> users = new List<IdentityUser>()
                {
                    new IdentityUser {
                    UserName = "user2@hotmail.com",
                    NormalizedUserName = "USER2@HOTMAIL.COM",
                    Email = "user2@hotmail.com",
                    NormalizedEmail = "USER2@HOTMAIL.COM",
                },
                new IdentityUser {
                    UserName = "user3@hotmail.com",
                    NormalizedUserName = "USER3@HOTMAIL.COM",
                    Email = "user3@hotmail.com",
                    NormalizedEmail = "USER3@HOTMAIL.COM",
                },
            };

            modelBuilder.Entity<IdentityUser>().HasData(users);

            ///----------------------------------------------------

            // Seed UserRoles

            List<IdentityUserRole<string>> userRoles = new List<IdentityUserRole<string>>();

            // Add Password For All Users

            users[0].PasswordHash = passwordHasher.HashPassword(users[0], "User.123");
            users[1].PasswordHash = passwordHasher.HashPassword(users[1], "User.155");

            userRoles.Add(new IdentityUserRole<string>
            {
                UserId = users[0].Id,
                RoleId = roles.First(q => q.Name == "User").Id
            });

            userRoles.Add(new IdentityUserRole<string>
            {
                UserId = users[1].Id,
                RoleId =  roles.First(q => q.Name == "Admin").Id
            });

            modelBuilder.Entity<IdentityUserRole<string>>().HasData(userRoles);

        }
    }
}
