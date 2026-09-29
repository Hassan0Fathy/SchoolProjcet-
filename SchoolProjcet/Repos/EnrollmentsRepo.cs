using School.AppContext;
using School.Models;
using SchoolProjcet.IGenericRepo;
using SchoolProjcet.IRepos;

namespace SchoolProjcet.Repos
{
    public class EnrollmentsRepo : GenericRepo<Enrollment>, IEnrollmentsRepo
    {
        private readonly AppDbContext _context;
        public EnrollmentsRepo(AppDbContext context) : base(context)
        {
            _context = context;
        }
        public Enrollment GetLastSubjectInEnrollmentDate(int subjectId)
        {
            var enrollment = db.Where(e => e.SubjectId == subjectId)
                               .OrderByDescending(e => e.EnrollmentDate)
                               .FirstOrDefault();
            if (enrollment == null)
            {
                throw new Exception("Enrollment not found");
            }
            return enrollment;

        }
    }
}
