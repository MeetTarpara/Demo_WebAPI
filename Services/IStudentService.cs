using DemoApi.Models;

namespace DemoApi.Services
{
    public interface IStudentService
    {
        IEnumerable<Student> GetStudents();
        Student? GetStudent(int id);
        void CreateStudent(Student student);

        bool UpdateStudent(int id,Student stu);
        bool DeleteStudent(int id);

    }
}
