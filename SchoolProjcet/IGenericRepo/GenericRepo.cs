using System.Runtime.Serialization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using School.AppContext;

namespace SchoolProjcet.IGenericRepo
{
    public class GenericRepo <T> : IGeneric<T>  where T : class
    {
        private readonly AppDbContext _context;
        public DbSet<T> db;
        
        public GenericRepo(AppDbContext context )
        {
            _context = context;
            db = _context.Set<T>();
        }

        public void CreateEntity(T entity)
        {
            db.Add(entity);
            _context.SaveChanges();
        }

        public ICollection<T> GetAll()
        {
            return db.ToList();
        }

        public T GetById(int id)
        {
            return db.Find(id);


        }
    }
}
