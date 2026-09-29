using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using School.AppContext;
using School.Models;
using SchoolProjcet.DTOs;
using SchoolProjcet.DTOs.StudentDto;
using SchoolProjcet.IGenericRepo;

namespace SchoolProjcet.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IGeneric<Student> _studentRepo;

        public StudentsController(AppDbContext context , IGeneric<Student> _studentRepo)
        {
            _context = context;
            this._studentRepo = _studentRepo;
        }

        [HttpGet]
        public IActionResult GetAllStudents()
        {
            var students = _studentRepo.GetAll()
                .Select(s => new StudentDto
                { 
                    Id = s.Id,
                    FirstName = s.FirstName,
                    LastName = s.LastName,
                    Email = s.Email,
                    PhoneNumber = s.PhoneNumber,
                    DateOfBirth = s.DateOfBirth,
                    ClassRoomId = s.ClassRoomId
                })
                .ToList();
            return Ok(students);
        }
        [HttpGet("{id}")]
        public IActionResult GetStudentById(int id)
        {
            var student = _studentRepo.GetById(id);
              

            if (student == null)
                return NotFound();

            return Ok(student);
        }
        [HttpPost]
        public IActionResult CreateStudent(CreateStudentDto dto)
        {
            if (dto == null)
                return BadRequest();

            var student = new Student
            {
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                Email = dto.Email,
                PhoneNumber = dto.PhoneNumber,
                DateOfBirth = dto.DateOfBirth,
                ClassRoomId = dto.ClassRoomId
            };

           _studentRepo.CreateEntity(student);
            

            return CreatedAtAction(nameof(GetStudentById), new { id = student.Id }, student);
        }
        [HttpPut("{id}")]
        public IActionResult UpdateStudent(int id, UpdateStudentDto dto)
        {
            if (dto == null)
                return BadRequest();

            var student = _context.Students.Find(id);
            if (student == null)
                return NotFound();

            student.FirstName = dto.FirstName;
            student.LastName = dto.LastName;
            student.Email = dto.Email;
            student.PhoneNumber = dto.PhoneNumber;
            student.DateOfBirth = dto.DateOfBirth;
            student.ClassRoomId = dto.ClassRoomId;

            _context.SaveChanges();

            return NoContent();
        }
        [HttpGet("Paged")]
        public IActionResult GetPagedStudents(int pageNumber = 1, int pageSize = 10)
        {
            var students = _context.Students
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .Select(s => new StudentDto
                {
                    Id = s.Id,
                    FirstName = s.FirstName,
                    LastName = s.LastName,
                    Email = s.Email,
                    PhoneNumber = s.PhoneNumber,
                    DateOfBirth = s.DateOfBirth,
                    ClassRoomId = s.ClassRoomId
                })
                .ToList();
            return Ok(students);
        }
        [HttpGet("Take")]
        public IActionResult GetStudentsTake(int count = 5)
        {
            var students = _context.Students
                .Take(count)
                .Select(s => new StudentDto
                {
                    Id = s.Id,
                    FirstName = s.FirstName,
                    LastName = s.LastName,
                    Email = s.Email,
                    PhoneNumber = s.PhoneNumber,
                    DateOfBirth = s.DateOfBirth,
                    ClassRoomId = s.ClassRoomId
                })
                .ToList();
            return Ok(students);
        }
        [HttpGet("Skip")]
        public IActionResult GetStudentsSkip(int count = 5)
        {
            var students = _context.Students
                .Skip(count)
                .Select(s => new StudentDto
                {
                    Id = s.Id,
                    FirstName = s.FirstName,
                    LastName = s.LastName,
                    Email = s.Email,
                    PhoneNumber = s.PhoneNumber,
                    DateOfBirth = s.DateOfBirth,
                    ClassRoomId = s.ClassRoomId
                })
                .ToList();
            return Ok(students);
        }
        [HttpGet("Immediate-demo")]
        public IActionResult Immadiate(int classRoomId)
        {
            var students = _context.Students
                   .Where(s => s.ClassRoomId == classRoomId)
                   .ToList();
            return Ok(students);

        }
        [HttpGet("Deferred-demo")]
        public IActionResult Deferred(int classRoomId)
        {
            var students = _context.Students
                   .Where(s => s.ClassRoomId == classRoomId);
            return Ok(students);
        }
        [HttpGet("With-ClassRoom")]
        public IActionResult JoinStudentsWithClassRooms(int classRoomId)
        {
            var students = _context.Students.Join(
                _context.ClassRooms,
                student => student.ClassRoomId,
                classRoom => classRoom.Id,
                (student, classRoom) => new
                {
                    StudentId = student.Id,
                    StudentName = student.FirstName + " " + student.LastName,
                    ClassRoomName = classRoom.Name
                })
                .Where(s => s.StudentId == classRoomId)
                .ToList();
            return Ok(students);
        }
        [HttpGet("To-Dictionary")]
        public IActionResult ToDictionary(int classRoomId)
        {
            var students = _context.Students
                .Where(s => s.ClassRoomId == classRoomId)
                .ToDictionary(s => s.Id, s => s.FirstName + " " + s.LastName);
            return Ok(students);
        }
        [HttpGet("To-List")]
        public IActionResult ToArray(int classRoomId)
        {
            var students = _context.Students
                .Where(s => s.ClassRoomId == classRoomId)
                .ToList();
            return Ok(students);
        }
        [HttpGet("Except")]
        public IActionResult Except(int classRoomId1, int classRoomId2)
        {
            var studentsInClassRoom1 = _context.Students
                .Where(s => s.ClassRoomId == classRoomId1)
                .Select(s => s.Id)
                .ToList();
            var studentsInClassRoom2 = _context.Students
                .Where(s => s.ClassRoomId == classRoomId2)
                .Select(s => s.Id)
                .ToList();
            var studentsNotInClassRoom = studentsInClassRoom1.Except(studentsInClassRoom2).ToList();
            return Ok(studentsInClassRoom2);
        }

        [HttpGet("Intersect")]
        public IActionResult Intersect(int classRoomId1, int classRoomId2)
        {
            var studentsInClassRoom1 = _context.Students
                .Where(s => s.ClassRoomId == classRoomId1)
                .Select(s => s.Id)
                .ToList();
            var studentsInClassRoom2 = _context.Students
                .Where(s => s.ClassRoomId == classRoomId2)
                .Select(s => s.Id)
                .ToList();
            var studentsInBothClassRooms = studentsInClassRoom1.Intersect(studentsInClassRoom2).ToList();
            return Ok(studentsInBothClassRooms);


        }
        [HttpGet("Union")]
        public IActionResult Union(int classRoomId1, int classRoomId2)
        {
            var studentsInClassRoom1 = _context.Students
                .Where(s => s.ClassRoomId == classRoomId1)
                .Select(s => s.Id)
                .ToList();
            var studentsInClassRoom2 = _context.Students
                .Where(s => s.ClassRoomId == classRoomId2)
                .Select(s => s.Id)
                .ToList();
            var allStudents = studentsInClassRoom1.Union(studentsInClassRoom2).ToList();
            return Ok(allStudents);
        }
        [HttpGet("Distinct")]   
        public IActionResult Distinct(int classRoomId)
        {
            var students = _context.Students
                .Where(s => s.ClassRoomId == classRoomId)
                .Select(s => s.FirstName)
                .Distinct()
                .ToList();
            return Ok(students);
        }
        [HttpGet("GroupBy")]
        public IActionResult GroupBy(int classRoomId)
        {
            var students = _context.Students
                .Where(s => s.ClassRoomId == classRoomId)
                .GroupBy(s => s.FirstName)
                .Select(g => new
                {
                    FirstName = g.Key,
                    Count = g.Count()
                })
                .ToList();
            return Ok(students);
        }
        [HttpGet("Names-String")]
        public IActionResult NamesString(int classRoomId)
        {
            var students = _context.Students
                .Where(s => s.ClassRoomId == classRoomId)
                .Select(s => s.FirstName + " " + s.LastName)
                .ToList();
            return Ok(students);
        }
     
    }

}
