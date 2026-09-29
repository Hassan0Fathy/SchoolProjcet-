using School.Models;
using SchoolProjcet.IGenericRepo;

namespace SchoolProjcet.IRepos
{
    public interface IClassRoomRepo : IGeneric<ClassRoom>
    {
        ClassRoom GetFirstClassroomByCapacity(int minCapacity);
        ClassRoom SearchName(string name);
    }
}
