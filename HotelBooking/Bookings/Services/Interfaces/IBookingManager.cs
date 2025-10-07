using Shared.Models;

namespace Bookings.Services.Interfaces
{
    public interface IBookingManager
    {
        /// <summary>
        /// Checks if a room is available in the given time
        /// </summary>
        /// <param name="req">Request for booking</param>
        /// <returns></returns>
        bool CheckRoomAvailability(BookingRequest req);

        /// <summary>
        /// Checks a room for availability and then books it if it is available in the given period
        /// </summary>
        /// <param name="req">Request for booking</param>
        int BookRoom(BookingRequest req);
    }
}
