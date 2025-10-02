namespace Shared.Interfaces
{
    public interface IRepository<T>
    {
        IEnumerable<T> GetAll();
        IQueryable<T> Query();
        T GetById(int id);
        void Add(T entity);
        void Update(T entity);
        void Delete(int id);
    }
}
