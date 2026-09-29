using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using School.AppContext;
using School.Models;
using SchoolProjcet.DTOs;
using SchoolProjcet.UnitOfWorks;

namespace SchoolProjcet.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EnrollmentsController : ControllerBase
    {
        private readonly IUnitOfWork _context;

        public EnrollmentsController(IUnitOfWork context )
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult GetAllEnrollments()
        {
            var enrollments = _context.Enrollments.GetAll()
                .Select(e => new EnrollmentDto
                {
                    Id = e.Id,
                    StudentId = e.StudentId,
                    SubjectId = e.SubjectId,
                    EnrollmentDate = e.EnrollmentDate,
                    Grade = e.Grade
                })
                .ToList();
            return Ok(enrollments);
        }
        [HttpGet("{id}")]
        public IActionResult GetEnrollmentById(int id)
        {
            var enrollment = _context.Enrollments.GetById(id);

            if (enrollment == null)
                return NotFound();

            return Ok(enrollment);
        }
        [HttpPost]
        public IActionResult CreateEnrollment(CreateEnrollmentDto dto)
        {
            if (dto == null)
                return BadRequest();

            var enrollment = new Enrollment
            {
                StudentId = dto.StudentId,
                SubjectId = dto.SubjectId,
                EnrollmentDate = dto.EnrollmentDate,
                Grade = dto.Grade
            };

          _context.Enrollments.CreateEntity(enrollment);    
            return CreatedAtAction(nameof(GetEnrollmentById), new { id = enrollment.Id }, enrollment);
        }
        [HttpPut("{id}")]
        public IActionResult UpdateEnrollment(int id, UpdateEnrollmentDto dto)
        {
            if (dto == null)
                return BadRequest();

            var enrollment = _context.Enrollments.GetById(id);
            if (enrollment == null)
                return NotFound();

            enrollment.StudentId = dto.StudentId;
            enrollment.SubjectId = dto.SubjectId;
            enrollment.EnrollmentDate = dto.EnrollmentDate;
            enrollment.Grade = dto.Grade;

            _context.Save();

            return NoContent();
        }

        [HttpGet("Enrollments-By-Descending-Grade")]
        public IActionResult GetEnrollmentsByDescendingGrade(int classId)
        {
            var enrollments = _context.Enrollments.GetAll()
                .Where(e=>e.Student.ClassRoomId == classId)
                .OrderByDescending(e => e.Grade)
                .Select(e => new EnrollmentDto
                {
                    Id = e.Id,
                    StudentId = e.StudentId,
                    SubjectId = e.SubjectId,
                    EnrollmentDate = e.EnrollmentDate,
                    Grade = e.Grade
                })
                .ToList();
            return Ok(enrollments);
        }
           [HttpGet("Min-Grade")]
        public IActionResult MinGrade(int classRoomId)
        {
            var minGrade = _context.Enrollments.GetAll()
                .Where(s => s.Student.ClassRoomId == classRoomId)
                .Min(s => s.Grade);
            return Ok(minGrade);
        }
        [HttpGet("Last-Enrollment-Date")]
        public IActionResult LastEnrollmentDate(int subjectId)
        {
            var last = _context.Enrollments.GetLastSubjectInEnrollmentDate(subjectId);
            return Ok(last);

        }

    }
}
