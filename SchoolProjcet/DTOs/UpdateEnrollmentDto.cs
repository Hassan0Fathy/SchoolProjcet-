namespace SchoolProjcet.DTOs
{
    public class UpdateEnrollmentDto
    {
        public int StudentId { get; set; }
        public int SubjectId { get; set; }
        public DateTime EnrollmentDate { get; set; }
        public decimal Grade { get; set; }
    }
}
