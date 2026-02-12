using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DemoApi.Data.Config
{
    public class ProjectConfig : IEntityTypeConfiguration<Project>
    {
        public void Configure(EntityTypeBuilder<Project> builder)
        {
            builder.ToTable("Projects");
            builder.HasKey(p => p.Id);

            builder.Property(p => p.Id).UseIdentityColumn();
            builder.Property(p => p.ProjectName).IsRequired().HasMaxLength(250);

     
            builder.HasData(new List<Project>
            {
                new Project { Id = 1, ProjectName = "Project A" },
                new Project { Id = 2, ProjectName = "Project B" }
            });

            // Configure Many-to-Many with Employee
            builder
                .HasMany(p => p.Employees)
                .WithMany(e => e.Projects) 
                .UsingEntity(j => j.ToTable("EmployeeProjects")); // Join table
        }
    }
}
