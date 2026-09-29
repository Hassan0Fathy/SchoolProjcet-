using School.Models;
using SchoolProjcet.IGenericRepo;
using SchoolProjcet.IRepos;

namespace SchoolProjcet.UnitOfWorks
{
    public interface IUnitOfWork
    {
        ITeacherRepo Teacher { get; }
        IClassRoomRepo ClassRoom { get; }
        IGeneric<Subject> Subject { get; }
        IGeneric<Student> Student { get; }
        IEnrollmentsRepo Enrollments { get; }

        void Save();

    }
}
