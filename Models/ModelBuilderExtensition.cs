using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeMangment.Models
{
    public static class ModelBuilderExtensition
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Employee>().HasData(

                new Employee
                {
                    Id = 1,
                    Name = "marry",
                    Email = "marray@aj.com",
                    Department = Dept.CTO
                },
                new Employee
                {
                    Id = 2,
                    Name = "John",
                    Email = "john@aj.com",
                    Department = Dept.HR
                }




                );
        }
    }
}
