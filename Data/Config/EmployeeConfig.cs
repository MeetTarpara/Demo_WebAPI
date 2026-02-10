using DemoApi.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DemoApi.Data.Config
{
    public class EmployeeConfig : IEntityTypeConfiguration<Employee> 
    {
        public void Configure(EntityTypeBuilder<Employee> builder)
        {
            builder.ToTable("Employees");
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id).UseIdentityColumn();
            builder.Property(n => n.Name).IsRequired().HasMaxLength(250);
            builder.Property(n=>n.Age).IsRequired();

            builder.HasData(new List<Employee>()
            {
                new Employee
                {
                    Id=1,
                    Name="Name1",
                    Age=18
                },
                  new Employee
                {
                    Id=2,
                    Name="Name2",
                   Age=45

                }
            });
        }
    }
    
}