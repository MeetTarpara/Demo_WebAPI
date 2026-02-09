using Microsoft.EntityFrameworkCore;

namespace DemoApi.Data
{
    public class CompanyDBContext : DbContext
    {
        public CompanyDBContext(){
            
        }
       DbSet<Employee> Employees {get; set;}
    }
}
