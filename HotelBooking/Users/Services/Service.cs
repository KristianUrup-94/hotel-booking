using Shared.Interfaces;
using Shared.Interfaces.BaseClasses;
using Users.Entity;

namespace Users.Services
{
    public class Service : SimpleService<User>, ISimpleService<User>
    {
        public Service(IRepository<User> repo) 
            : base(repo)
        {
        }
    }
}
