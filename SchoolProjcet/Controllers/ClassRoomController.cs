using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using School.AppContext;
using School.Models;
using SchoolProjcet.DTOs;
using SchoolProjcet.UnitOfWorks;

namespace SchoolProjcet.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClassRoomController : ControllerBase
    {
       
        private readonly IUnitOfWork _unitofwork;

        public ClassRoomController(IUnitOfWork  context)
        {
            _unitofwork = context;
        }

        [HttpGet]
        public IActionResult GetAllClassRooms()
        {
            var classRooms = _unitofwork.ClassRoom.GetAll()
                .Select(c => new ClassRoomDto
                {
                    Id = c.Id,
                    Name = c.Name,
                    GradeLevel = c.GradeLevel,
                    Capacity = c.Capacity
                })
                .ToList();
            return Ok(classRooms);
        }

        [HttpGet("{id}")]


        public IActionResult GetClassRoomDetails(int id)
        {
            var classRoom = _unitofwork.ClassRoom.GetById(id);

            if (classRoom == null)
                return NotFound();

            return Ok(classRoom);
        }
        [HttpPost]
        public IActionResult CreateClassRoom(CreateClassRoomDto dto)
        {
            if (dto == null)
                return BadRequest();

            var classRoom = new ClassRoom
            {
                Name = dto.Name,
                GradeLevel = dto.GradeLevel,
                Capacity = dto.Capacity
            };

            _unitofwork.ClassRoom.CreateEntity(classRoom);
            _unitofwork.Save();

            return CreatedAtAction(nameof(GetClassRoomDetails), new { id = classRoom.Id }, classRoom);


        }
        [HttpPut("{id}")]
        public IActionResult UpdateClassRoom(int id, UpdateClassRoomDto dto)
        {
            if (dto == null)
                return BadRequest();

            var classRoom = _unitofwork.ClassRoom.GetById(id);
            if (classRoom == null)
                return NotFound();

            classRoom.Name = dto.Name;
            classRoom.GradeLevel = dto.GradeLevel;
            classRoom.Capacity = dto.Capacity;

            _unitofwork.Save();

            return NoContent();
        }
       
        [HttpGet("To-Array")]

        public IActionResult ToArray(int classRoomId)
        {
            var students = _unitofwork.Student.GetAll()
                .Where(s => s.ClassRoomId == classRoomId)
                .ToArray();
            return Ok(students);
        }
        [HttpGet("GetFirstClassroomByCapacity")]
            public IActionResult GetFirstClassroomByCapacity(int capcity)
            {
            var classroom = _unitofwork.ClassRoom.GetFirstClassroomByCapacity(capcity);
            if (classroom == null)
                {
                    return NotFound(new { Message = $"No classroom found with capacity >= {capcity}" });
                }

                return Ok(classroom);
            }
        [HttpGet("SearchName")]
        public IActionResult SearchName(string name)
        {
            var classroom = _unitofwork.ClassRoom.SearchName(name);
            if (classroom == null)
            {
                return NotFound(new { Message = $"No classroom found with name '{name}'" });
            }
            return Ok(classroom);
        }




    }
}
