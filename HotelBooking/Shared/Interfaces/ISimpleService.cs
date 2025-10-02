namespace Rooms
{
    public interface ISimpleService<T>
    {
        List<T> GetAll();
        T Get(int id);
        void CreateRoom(T entity);
        void DeleteRoom(int id);
        void UpdateRoom(T entity);

    }
}
