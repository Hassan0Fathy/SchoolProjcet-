using Microsoft.EntityFrameworkCore;
using School.AppContext;
using School.Models;
using SchoolProjcet.IGenericRepo;
using SchoolProjcet.IRepos;

namespace SchoolProjcet.UnitOfWorks
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _context;
        public UnitOfWork(AppDbContext context, IClassRoomRepo classRoomRepo, IEnrollmentsRepo enrollmentsRepo, IGeneric<Subject> subjectRepo, IGeneric<Student> studentRepo)
        {
            _context = context;
            ClassRoom = classRoomRepo;
            Subject = subjectRepo;
            Student = studentRepo;
            Enrollments = enrollmentsRepo;

        }
        public ITeacherRepo Teacher { get; private set; }

        public IClassRoomRepo ClassRoom { get; private set; }

        public IGeneric<Subject> Subject { get; }

        public IGeneric<Student> Student { get; }
        public IEnrollmentsRepo Enrollments { get; }


        public void Save()
        {
            _context.SaveChanges();
        }
    }
}
