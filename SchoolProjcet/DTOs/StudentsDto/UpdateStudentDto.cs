namespace SchoolProjcet.DTOs
{
    public class UpdateStudentDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string? PhoneNumber { get; set; }
        public DateOnly DateOfBirth { get; set; }
        public int ClassRoomId { get; set; }
    }
}
