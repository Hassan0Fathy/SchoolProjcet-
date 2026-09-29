using School.Models;
using SchoolProjcet.IGenericRepo;

namespace SchoolProjcet.IRepos
{
    public interface ISubjectRepo : IGeneric<Subject>
    {
        Subject GetTheFirstSubject();

    }
}
