using School.Models;
using SchoolProjcet.DTOs.StudentDto;

namespace SchoolProjcet.DTOs
{
    public class ClassRoomDetailsDto
    {
        public string Name { get; set; }

        public ICollection<StudentDtos> Students { get; set; }

    }
}
