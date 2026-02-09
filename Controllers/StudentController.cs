using Microsoft.AspNetCore.Mvc;
using DemoApi.Models;
using DemoApi.Services;

namespace DemoApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StudentsController : ControllerBase
    {
        private readonly IStudentService _service;

        public StudentsController(IStudentService service)
        {
            _service = service;
        }


        [HttpGet]
        public IActionResult GetAll()
        {
            var students = _service.GetStudents();

            var response = new ApiResponse<IEnumerable<Student>>(
                StatusCodes.Status200OK,
                "Students fetched successfully",
                students
            );
            return Ok(response);
            // return Ok(_service.GetStudents());
        }


        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
           var student = _service.GetStudent(id);

        if (student == null)
        {
            return NotFound(new ApiResponse<Student>(
                StatusCodes.Status404NotFound,
                "Student not found",
                null,
                "Invalid student id"
            ));
        }

        return Ok(new ApiResponse<Student>(
            StatusCodes.Status200OK,
            "Student fetched successfully",
            student
        ));
        }


        [HttpPost]
        public IActionResult Create(Student student)
        {
            //Fluent Validation
            if(!ModelState.IsValid)
             {
                return BadRequest(ModelState);
            }
            _service.CreateStudent(student);
            return Ok(student);
        }

        

        [HttpPut("{id}")]
        public IActionResult Update(int id, Student student)
        {
            if (!_service.UpdateStudent(id, student))
                return NotFound();

            return NoContent();
        }


        [HttpDelete("{id}")]
        public IActionResult Delete(int id){

            if(!_service.DeleteStudent(id))
                return NotFound();
            return NoContent();
        }
    }
}
