using HrCrud.Models;
using Microsoft.EntityFrameworkCore;

namespace HrCrud.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> opts) : base(opts) { }
        public DbSet<Employee> Employee => Set<Employee>();

        protected override void OnModelCreating(ModelBuilder b)
        {
            b.Entity<Employee>().HasIndex(e => e.NIK).IsUnique();
            // b.Entity<Employee>().HasData(
            //     new Employee {
            //         Id=1, NIK="EMP001", Name="Alya Putri",
            //         PlaceOfBirth="Bandung", DateOfBirth=new DateTime(1998,5,12),
            //         BasicSalary=7000000, Gender=Gender.Female, MaritalStatus=MaritalStatus.Single
            //     },
            //     new Employee {
            //         Id=2, NIK="EMP002", Name="Bima Pratama",
            //         PlaceOfBirth="Surabaya", DateOfBirth=new DateTime(1995,11,3),
            //         BasicSalary=9000000, Gender=Gender.Male, MaritalStatus=MaritalStatus.Married
            //     }
            // );
        }
    }
}