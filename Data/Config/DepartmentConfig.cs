using DemoApi.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DemoApi.Data.Config
{
    public class DepartmentConfig : IEntityTypeConfiguration<Department> 
    {
        public void Configure(EntityTypeBuilder<Department> builder)
        {
            builder.ToTable("Departments");
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id).UseIdentityColumn();
            builder.Property(n => n.DepartmentName).IsRequired().HasMaxLength(250);

            builder.HasData(new List<Department>()
            {
                new Department
                {
                    Id=1,
                    DepartmentName="IT"
                },
                  new Department
                {
                    Id=2,
                    DepartmentName="Management"

                }
            });
        }
    }
    
}