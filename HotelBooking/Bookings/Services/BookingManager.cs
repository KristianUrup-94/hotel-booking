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

        /// <summary>
        /// Checks if a room is available in the given time
        /// </summary>
        /// <param name="req">Request for booking</param>
        /// <returns></returns>
        public bool CheckRoomAvailability(BookingRequest req)
        {
            if(req.From >= req.To)
            {
                throw new InvalidDataException("The to date is before from date");
            }
            if (req.From < DateTimeOffset.Now) 
            {
                throw new InvalidDataException("The from date is already exceeded");
            }
            IQueryable<Booking> result = _repo.Query()
                .Where(x => x.RoomId == req.RoomId)
                .Where(x => (x.To < req.From) || (x.From > req.To));
            
            return result.Any();
        }

        /// <summary>
        /// Checks a room for availability and then books it if it is available in the given period
        /// </summary>
        /// <param name="req">Request for booking</param>
        public int BookRoom(BookingRequest req)
        {

            if (CheckRoomAvailability(req) == false)
            {
                throw new InvalidDataException("Room is not available");
            }
            var booking = new Booking
            {
                BookingId = 10012,
                Comments = "Bring a beer to the table",
                To = req.To,
                From = req.From,
                RoomId = req.RoomId
            };
            _repo.Add(booking);
            return booking.Id;
        }
    }
}
