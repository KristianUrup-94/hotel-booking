using Bookings.Entity;
using Shared.Interfaces;
using Shared.Interfaces.BaseClasses;

namespace Bookings.Services
{
    public class Service : SimpleService<Booking>, ISimpleService<Booking>
    {
        public Service(IRepository<Booking> repo) 
            : base(repo)
        {
        }
    }
}
