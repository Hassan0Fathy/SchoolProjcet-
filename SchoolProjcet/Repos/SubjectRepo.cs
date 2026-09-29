using School.AppContext;
using School.Models;
using SchoolProjcet.IGenericRepo;
using SchoolProjcet.IRepos;

namespace SchoolProjcet.Repos
{
    public class SubjectRepo : GenericRepo<Subject>, ISubjectRepo
    {
        private readonly AppDbContext _context;
        public SubjectRepo(AppDbContext context) : base(context)
        {
            _context = context;
        }
        public Subject GetTheFirstSubject()
        {
            return db.OrderBy(s => s.Name).FirstOrDefault();
        }

    }

}
