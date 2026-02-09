using DemoApi.Models;

namespace DemoApi.Repositories
{
    public interface IStudentRepository
    {
        IEnumerable<Student> GetAll();
        Student? GetById(int id);
        void Add(Student student);
         bool Update(int id,Student stu);
        bool Delete(int id);
    }
}
