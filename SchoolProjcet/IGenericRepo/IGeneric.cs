namespace SchoolProjcet.IGenericRepo
{
    public interface IGeneric <T> where T : class
    {
        ICollection<T> GetAll();
        T GetById(int id);
        void CreateEntity(T entity);
    }
}
