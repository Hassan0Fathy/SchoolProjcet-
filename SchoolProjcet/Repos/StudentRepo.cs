using School.AppContext;
using School.Models;
using SchoolProjcet.IRepos;

namespace SchoolProjcet.Repos
{
    public class StudentRepo : IGenericRepo<Student>, IStudentRepo
    {
        private readonly AppDbContext _context;
        public StudentRepo(AppDbContext context) : base(context)
        {
            _context = context;
        }
        public Student GetLastOrDefault(int teacherId)
        {

        }

    }
}
