using DemoApi.Data;

namespace DemoApi.Services
{
    public class EmployeeService
    {
        private readonly CompanyDBContext _dbContext;
          public EmployeeService(CompanyDBContext dBContext)
        {
            _dbContext=dBContext;
        }
        public List<Employee> GetAll()
        {
            return _dbContext.Employees.ToList();
        }

        public Employee? GetById(int id)
        {
            return _dbContext.Employees.FirstOrDefault(e => e.Id == id);
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

            existing.Name = employee.Name;
            existing.Age = employee.Age;
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
