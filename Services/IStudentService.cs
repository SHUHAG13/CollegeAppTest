using CollegeAppTest;
using CollegeAppTest.Models;
using Microsoft.EntityFrameworkCore;

namespace CollegeApp.Services
{
    public interface IStudentService
    {
        Task<IEnumerable<Student>> GetAllStudents();
        Task<Student> GetStudentById(int id);
        Task<Student> AddStudent(Student student);
        Task<Student> UpdateStudent(int id, Student student);
        Task<bool> DeleteStudent(int id);
      



    }
    public class StudentService : IStudentService
    {
        private readonly AppDbContext _context;



        public StudentService(AppDbContext dbContext)
        {
            _context = dbContext;
        }
        public async Task<IEnumerable<Student>> GetAllStudents()
        {
            return await _context.Students.ToListAsync();
        }




        public async Task<Student>GetStudentById(int id)
        {
            return await _context.Students.FindAsync(id);
        }

        public async Task<Student>AddStudent(Student student)
        {
            _context.Students.Add(student);
            await _context.SaveChangesAsync();
            return student;
        }



      
        public async Task<Student>UpdateStudent(int id,Student student)
        {
            var existingStudent = await _context.Students.FindAsync(id);
            if(existingStudent==null)
            {
                return null;
            }
            existingStudent.Name = student.Name;
            _context.Students.Update(existingStudent);
            await _context.SaveChangesAsync();
            return existingStudent;
        }





        public async Task<bool>DeleteStudent(int id)
        {
            var student = await _context.Students.FindAsync(id);
            if(student==null)
            {
                return false;
            }
            _context.Students.Remove(student);
            await _context.SaveChangesAsync();
            return true;
        }
       



    }
}