namespace SchoolProjcet.DTOs
{
    public class CreateSubjectDto
    {
        public string Name { get; set; }
        public string? Description { get; set; }
        public int MaxGrade { get; set; }
        public int TeacherId { get; set; }
    }
}
