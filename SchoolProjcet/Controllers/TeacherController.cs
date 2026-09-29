using Microsoft.AspNetCore.Mvc;
using School.AppContext;
using School.Models;
using SchoolProjcet.DTOs;
using SchoolProjcet.DTOs.TeacherDto;
using SchoolProjcet.IGenericRepo;
using SchoolProjcet.IRepos;

namespace SchoolProjcet.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TeacherController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly ITeacherRepo _teacherRepo;


        public TeacherController(AppDbContext context , IGeneric<Teacher> _TeacherRepo, ITeacherRepo teacherRepo)
        {
            _context = context;
            _teacherRepo = teacherRepo;
        }

        [HttpGet]
        public IActionResult GetAllTeachers()
        {
            var teachers = _teacherRepo.GetAll()
                .Select(t => new TeacherDto
                {
                    Id = t.Id,
                    FirstName = t.FirstName,
                    LastName = t.LastName,
                    Email = t.Email,
                    PhoneNumber = t.PhoneNumber,
                    Salary = t.Salary,
                    DepartmentId = t.DepartmentId
                })
                .ToList();
            return Ok(teachers);
        }
        [HttpGet("MinSalary")]
        public IActionResult GetTeachersWithMinSalary(decimal salary)
        {
            var teachers = _teacherRepo.MinSalary(salary)
                .Select(t => new TeacherDto
                {
                    Id = t.Id,
                    FirstName = t.FirstName,
                    LastName = t.LastName,
                    Email = t.Email,
                    PhoneNumber = t.PhoneNumber,
                    Salary = t.Salary,
                    DepartmentId = t.DepartmentId
                })
                .ToList();
            return Ok(teachers);
        }
        [HttpGet("{id}")]
        public IActionResult GetTeacherById(int id)
        {
            var teacher = _teacherRepo.GetById(id);
               
            if (teacher == null)
                return NotFound();

            return Ok(teacher);
        }
        [HttpPost]
        public IActionResult CreateTeacher(CreateTeacherDto dto)
        {
            if (dto == null)
                return BadRequest();

            var teacher = new Teacher
            {
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                Email = dto.Email,
                PhoneNumber = dto.PhoneNumber,
                Salary = dto.Salary,
                DepartmentId = dto.DepartmentId
            };

            _teacherRepo.CreateEntity(teacher);

            return CreatedAtAction(nameof(GetTeacherById), new { id = teacher.Id }, teacher);
        }
        [HttpPut("{id}")]
        public IActionResult UpdateTeacher(int id, UpdateTeacherDto dto)
        {
            if (dto == null)
                return BadRequest();

            var teacher = _context.Teachers.Find(id);
            if (teacher == null)
                return NotFound();

            teacher.FirstName = dto.FirstName;
            teacher.LastName = dto.LastName;
            teacher.Email = dto.Email;
            teacher.PhoneNumber = dto.PhoneNumber;
            teacher.Salary = dto.Salary;
            teacher.DepartmentId = dto.DepartmentId;

            _context.SaveChanges();

            return NoContent();
        }

        [HttpGet("GetByEmail")]
        public IActionResult GetTeacherByEmail(string email)
        {
            var teacher = _teacherRepo.TeacherEmail(email);
          
            return Ok(teacher);
        }
    }
}
