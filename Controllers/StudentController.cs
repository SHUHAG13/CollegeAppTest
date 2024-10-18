using CollegeApp.Services;
using CollegeAppTest.Models;
using Microsoft.AspNetCore.Mvc;

namespace CollegeApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly IStudentService _studentService;

        public StudentController(IStudentService studentService)
        {
            _studentService = studentService;
        }

        [HttpGet]
        public async Task<ActionResult> GetAllStudents()
        {
            var students = await _studentService.GetAllStudents();
            return Ok(students);

        }


        [HttpGet("{id:int}")]
        public async Task<ActionResult<Student>> GetStudentById(int id)
        {
            var student = await _studentService.GetStudentById(id);
            if (student == null)

                return NotFound($"Student with id {id} not found");

            return Ok(student);
        }



        [HttpPost]
        public async Task<ActionResult<Student>> AddStudent([FromBody] Student student)
        {
            if (student == null)
            {
                return BadRequest("Student data is invalid");
            }
            var createdStudent = await _studentService.AddStudent(student);
            return CreatedAtAction(nameof(GetAllStudents), new { id = createdStudent.Id }, createdStudent);

        }
        [HttpPut("{id:int}")]
        public async Task<ActionResult<Student>> UpdateStudent(int id, [FromBody] Student student)
        {
            if (id <= 0 || student == null)
            {
                return BadRequest("Invalid data provided");
            }
            var updateStudent = await _studentService.UpdateStudent(id, student);
            if (updateStudent == null)
            {
                return NotFound($"Student with id {id} is not found");
            }
            return Ok(updateStudent);

        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult>DeleteStudent(int id)
        {
            var result = await _studentService.DeleteStudent(id);
            if(!result)
            {
                return NotFound($"Student with id {id} not found");

            }
            return Ok($"Student with id {id} has been deleted");
        }

       
        
 



    }
}
