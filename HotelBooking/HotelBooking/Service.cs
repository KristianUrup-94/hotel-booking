using Shared.Interfaces;
using Shared.Interfaces.BaseClasses;

namespace Rooms
{
    public class Service : SimpleService<Room>, ISimpleService<Room>
    {
        public Service(IRepository<Room> repo) 
            : base(repo)
        {
        }
    }
}
