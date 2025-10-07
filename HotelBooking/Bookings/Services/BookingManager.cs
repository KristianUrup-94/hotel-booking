using Bookings.Entity;
using Bookings.Services.Interfaces;
using Shared.Interfaces;
using Shared.Models;

namespace Bookings.Services
{
    public class BookingManager : IBookingManager
    {
        private readonly IRepository<Booking> _repo;

        public BookingManager(IRepository<Booking> repo) 
        { 
            _repo = repo;
        }

        public void BookRoom(BookingRequest req)
        {
            throw new NotImplementedException();
        }

        public bool CheckRoomAvailability(BookingRequest req)
        {
            throw new NotImplementedException();
        }
    }
}
