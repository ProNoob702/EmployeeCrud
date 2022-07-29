
using EmployeeCrud.Domain;
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
           
        }
    }
}
