using DemoApi.Models;
using DemoApi.Repositories;

namespace DemoApi.Services
{
    public class StudentService : IStudentService
    {
        private readonly IStudentRepository _repository;

        public StudentService(IStudentRepository repository)
        {
            _repository = repository;
        }

        public IEnumerable<Student> GetStudents()
        {
            return _repository.GetAll();
        }

        public Student? GetStudent(int id)
        {
            return _repository.GetById(id);
        }

        public void CreateStudent(Student student)
        {
            _repository.Add(student);
        }


        public bool UpdateStudent(int id, Student stu){

          return _repository.Update(id,stu);
        }

        public bool DeleteStudent(int id){
            return _repository.Delete(id);
        }

    }
}
