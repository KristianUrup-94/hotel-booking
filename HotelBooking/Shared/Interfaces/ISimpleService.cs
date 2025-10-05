namespace Shared.Interfaces
{
    public interface ISimpleService<T>
    {
        List<T> GetAll();
        T Get(int id);
        void Create(T entity);
        void Delete(int id);
        void Update(T entity);

    }
}
