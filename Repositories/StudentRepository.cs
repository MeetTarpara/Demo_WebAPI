using DemoApi.Models;

namespace DemoApi.Repositories
{
    public class StudentRepository : IStudentRepository
    {
       
        private readonly List<Student> _students = new()
        {
            new Student { Id = 1, Name = "Meet", Age = 20 },
            new Student { Id = 2, Name = "XYZ", Age = 22 }
        };

        public IEnumerable<Student> GetAll()
        {
            return _students;
        }

        public Student? GetById(int id)
        {
            return _students.FirstOrDefault(s => s.Id == id);
        }

        public void Add(Student student)
        {
            student.Id = _students.Max(s => s.Id) + 1;
            _students.Add(student);
        }

        public bool Update(int id, Student stu){

            var existing = GetById(id);
            if(existing==null) return false;

            existing.Name=stu.Name;
            existing.Age=stu.Age;
            return true;
        }

        public bool Delete(int id){
            var stu = GetById(id);
            if(stu==null) return false;

            _students.Remove(stu);
            return true;
        }
    }
}
