using System.ComponentModel.DataAnnotations;

namespace SchoolProjcet.DTOs
{
    public class ClassRoomDto
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int GradeLevel { get; set; }

        public int Capacity { get; set; }
    }
}
