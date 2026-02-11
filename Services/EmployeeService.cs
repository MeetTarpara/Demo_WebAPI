using DemoApi.Data;
using Microsoft.EntityFrameworkCore;

namespace DemoApi.Services
{
    public class EmployeeService
    {
        private readonly CompanyDBContext _dbContext;
          public EmployeeService(CompanyDBContext dBContext)
        {
            _dbContext=dBContext;
        }

        // public async Task<List<Employee>> GetAll()
        // {
        //     return await _dbContext.Employees
        //                             .Include(e => e.Department)
        //                             .Include(e => e.Projects)
        //                             .ToListAsync();
        // }

        public async Task<List<EmployeeResponse>> GetAll()
        {
            var employees = await _dbContext.Employees
                .AsNoTracking()
                .Select(s => new EmployeeResponse
                {
                    Id = s.Id,
                    Name = s.Name,
                    Age = s.Age,
                    DepartmentId = s.DepartmentId,
                    Department = s.Department != null ? s.Department.DepartmentName : null,
                    Projects = s.Projects
                        .Select(p => new ProjectResponse
                        {
                            Id = p.Id,
                            ProjectName = p.ProjectName
                        })
                        .ToList()
                })
                .ToListAsync();

            return employees;
        }


        public Employee? GetById(int id)
        {
            return _dbContext.Employees.AsNoTracking().FirstOrDefault(e => e.Id == id);
        }

        public Employee Create(Employee employee)
        {
            var newEmployee = new Employee
            {
                Name = employee.Name,
                Age = employee.Age
            };

           
            _dbContext.Employees.Add(newEmployee);
            _dbContext.SaveChanges();
            return newEmployee;
        }

        public bool Update(int id, Employee employee)
        {
            var existing = GetById(id);
            if (existing == null) return false;

              var newRecord= new Employee(){
                Id=existing.Id,
                Name = employee.Name,
                Age = employee.Age

            };
            _dbContext.Employees.Update(newRecord);

            //alternate way 
            // existing.Name = employee.Name;
            // existing.Age = employee.Age;
            _dbContext.SaveChanges();
            return true;
        }

        public bool Delete(int id)
        {
            var employee = GetById(id);
            if (employee == null) return false;

            _dbContext.Employees.Remove(employee);
            _dbContext.SaveChanges();
            return true;
        }
    }
}
