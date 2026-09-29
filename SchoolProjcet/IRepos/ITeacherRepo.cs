using School.Models;
using SchoolProjcet.IGenericRepo;

namespace SchoolProjcet.IRepos
{
    public interface ITeacherRepo : IGeneric<Teacher>
    {
        ICollection<Teacher> MinSalary(decimal salary);
        Teacher TeacherEmail(string email);

    }
}
