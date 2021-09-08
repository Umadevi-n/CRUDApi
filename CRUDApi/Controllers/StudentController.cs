using CRUDApi.Model;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRUDApi.Controllers
{
    [Route("api/[Controller]")]
    public class StudentController : Controller
    {
        private Context _context;
        public StudentController(Context context)
        {
            _context = context;

        }
        [HttpGet]
        public List<Student> Get()
        {
            return _context.students.ToList();

        }
        [HttpGet("{Id}")]
        public Student GetStudent(int Id)
        {
            var student = _context.students.Where(a => a.id == Id).SingleOrDefault();
            return student;
        }
        [HttpPost]
        public IActionResult PostStudent([FromBody] Student student)
        {
            if (!ModelState.IsValid)
                return BadRequest("Not a Valid Model");
            _context.students.Add(student);
            _context.SaveChanges();
            return Ok();
        }
        [HttpDelete("{Id}")]
        public async Task<IActionResult> DeleteStudent(int Id)
        {
            var students = await _context.students.FindAsync(Id);
            if (students == null)
            {
                return NotFound();
            }
                _context.students.Remove(students);
                await _context.SaveChangesAsync();
                return NoContent();
            }
        
            


    }
}
