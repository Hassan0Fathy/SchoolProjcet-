using Microsoft.EntityFrameworkCore;
using School.AppContext;
using School.Models;
using SchoolProjcet.IGenericRepo;
using SchoolProjcet.IRepos;

namespace SchoolProjcet.Repos
{
    public class ClassRoomRepo : GenericRepo<ClassRoom>, IClassRoomRepo
    {
        private readonly AppDbContext _context;
        public ClassRoomRepo(AppDbContext context) : base(context)
        {
            _context = context;
        }
        public ClassRoom GetFirstClassroomByCapacity(int minCapacity)
        {
            return _context.ClassRooms
                .Where(c => c.Capacity >= minCapacity)
                .OrderBy(c => c.Capacity)
                .FirstOrDefault();
        }
        public ClassRoom SearchName(string name)
        {
            var classroom = db.SingleOrDefault(c => c.Name == name);
            if (classroom == null)
            {
                throw new Exception("Classroom not found");
            }
            return classroom;
        }
    }
}
