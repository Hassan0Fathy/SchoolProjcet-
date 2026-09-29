using Microsoft.AspNetCore.Http.HttpResults;
using School.AppContext;
using School.Models;
using SchoolProjcet.IGenericRepo;
using SchoolProjcet.IRepos;

namespace SchoolProjcet.Repos
{
    public class TeacherRepo : GenericRepo<Teacher>, ITeacherRepo
    {
        private readonly AppDbContext _context;
        public TeacherRepo(AppDbContext context) : base(context)
        {
            _context = context;
        }
        public ICollection<Teacher> MinSalary(decimal salary)
        {
            return db.Where(t => t.Salary > salary).ToList();
        }
        public Teacher TeacherEmail(string email)
        {
            var teacher = db.Where(t => t.Email == email);
            if (teacher == null)
            {
                throw new Exception("Teacher not found");
            }
            return teacher.FirstOrDefault();
        }


    }
}
