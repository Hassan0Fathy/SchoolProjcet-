using School.Models;
using SchoolProjcet.IGenericRepo;

namespace SchoolProjcet.IRepos
{
    public interface IEnrollmentsRepo : IGeneric<Enrollment>
    {
        Enrollment GetLastSubjectInEnrollmentDate(int subjectId);

    }
}
