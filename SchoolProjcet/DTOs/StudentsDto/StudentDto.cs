namespace SchoolProjcet.DTOs.StudentDto
{
    public class StudentDto
    {

        public int Id { get; set; }
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public required string Email { get; set; }
        public string? PhoneNumber { get; set; }
        public DateOnly DateOfBirth { get; set; }
        public int ClassRoomId { get; set; }
    }
}
