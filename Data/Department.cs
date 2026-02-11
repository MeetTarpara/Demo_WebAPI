using System.Text.Json.Serialization;
using DemoApi.Models;

namespace DemoApi.Data
{
    public class Department
    {
        public int Id { get; set; }
        public string DepartmentName { get; set; } = string.Empty;


        // We use [JsonIgnore] to prevent circular reference during JSON serialization 
        // in bidirectional relationships like one-to-many.
        public virtual ICollection<Employee> Employees {get; set;}
    }
}
