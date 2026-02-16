using DemoApi.Data.Config;
using Microsoft.EntityFrameworkCore;

namespace DemoApi.Data
{
    public class CompanyDBContext : DbContext
    {

        // Allows EF Core to connect to DB
        public CompanyDBContext(DbContextOptions<CompanyDBContext> options):base(options)
        {
            
        }

        // Represents a database table
        public DbSet<Employee> Employees {get; set;}
        public DbSet<Department> Departments {get; set;}
        public DbSet<Project> Projects {get; set;}
        public DbSet<Person> Persons {get; set;}



        //EF Core builds a model of your database at runtime
        // This method lets you customize that model
        // Customize DB structure
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.ApplyConfiguration(new EmployeeConfig());
            modelBuilder.ApplyConfiguration(new DepartmentConfig());
            modelBuilder.ApplyConfiguration(new ProjectConfig());
        }
    }
}
