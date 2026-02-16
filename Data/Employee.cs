
using System.Text.Json.Serialization;

namespace DemoApi.Data
{
    public class Employee
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public int Age { get; set; }
        public int? DepartmentId { get; set; }
        public Department? Department { get; set; }

        // Many-to-Many
        public ICollection<Project> Projects { get; set; } = new List<Project>();
    }

//Like DTO response model to avoid recursion
    public class EmployeeResponse
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public int Age { get; set; }
        public int? DepartmentId { get; set; }
        public string? Department { get; set; }
        public List<ProjectResponse> Projects { get; set; } = new List<ProjectResponse>();
    }

    public class ProjectResponse
    {
        public int Id { get; set; }
        public string ProjectName { get; set; } = string.Empty;
    }

}
