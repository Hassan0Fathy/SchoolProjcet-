using School.Models;
using SchoolProjcet.IGenericRepo;

namespace SchoolProjcet.IRepos
{
    public interface IStudentRepo : IGeneric<IStudentRepo>
    {
        Student GetLastOrDefault(int teacherId);

    }
}
