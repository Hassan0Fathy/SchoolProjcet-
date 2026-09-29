using Microsoft.AspNetCore.Mvc;
using School.AppContext;
using School.Models;
using SchoolProjcet.DTOs;
using SchoolProjcet.IRepos;

namespace SchoolProjcet.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubjectsController : ControllerBase
    {
        private readonly AppDbContext _context;

        private readonly ISubjectRepo subjectRepo;

        public SubjectsController(AppDbContext context , ISubjectRepo subjectRepo)
        {
            _context = context;
            this.subjectRepo = subjectRepo;
        }

        [HttpGet]
        public IActionResult GetAllSubjects()
        {
            var subjects = subjectRepo.GetAll()
                .Select(s => new SubjectDto
                {
                    Id = s.Id,
                    Name = s.Name,
                    Description = s.Description,
                    MaxGrade = s.MaxGrade,
                    TeacherId = s.TeacherId
                    
                })
                .ToList();
            return Ok(subjects);
        }
        [HttpGet("{id}")]
        public IActionResult GetSubjectById(int id)
        {
            var subject = subjectRepo.GetById(id);
                

            if (subject == null)
                return NotFound();

            return Ok(subject);
        }
        [HttpPost]
        public IActionResult CreateSubject(CreateSubjectDto dto)
        {
            if (dto == null)
                return BadRequest();

            var subject = new Subject
            {
                Name = dto.Name,
                Description = dto.Description,
                MaxGrade = dto.MaxGrade,
                TeacherId = dto.TeacherId
            };

            subjectRepo.CreateEntity(subject);
            return CreatedAtAction(nameof(GetSubjectById), new { id = subject.Id }, subject);
        }
        [HttpGet("FirstSubject")]   
        public IActionResult GetTheFirstSubject()
        {
            var subject = subjectRepo.GetTheFirstSubject();
            if (subject == null)
                return NotFound();
            return Ok(subject);
        }


    }
}
