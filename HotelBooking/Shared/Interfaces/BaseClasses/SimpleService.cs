
namespace Shared.Interfaces.BaseClasses
{
    public abstract class SimpleService<T> : ISimpleService<T> where T : class
    {
        private readonly IRepository<T> _repo;

        public SimpleService(IRepository<T> repo) 
        {
            _repo = repo;
        }
        public void CreateRoom(T entity)
        {
            _repo.Add(entity);
        }

        public void DeleteRoom(int id)
        {
            _repo.Delete(id);
        }

        public T Get(int id)
        {
            return _repo.GetById(id);
        }

        public List<T> GetAll()
        {
            return _repo.GetAll().ToList();
        }

        public void UpdateRoom(T room)
        {
            _repo.Update(room);
        }
    }
}
