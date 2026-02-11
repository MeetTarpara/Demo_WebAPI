using System.Text.Json.Serialization;

namespace DemoApi.Data
{
    public class Project
    {
        public int Id { get; set; }
        public string ProjectName { get; set; } = string.Empty;


        // Many-to-Many relationship
        public ICollection<Employee> Employees { get; set; } = new List<Employee>();
    }
}
