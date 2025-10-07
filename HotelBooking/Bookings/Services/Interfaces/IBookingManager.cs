using Shared.Models;

namespace Bookings.Services.Interfaces
{
    public interface IBookingManager
    {
        /// <summary>
        /// Checks if a room is available in the given time
        /// </summary>
        /// <param name="roomId"></param>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <returns></returns>
        bool CheckRoomAvailability(BookingRequest req);

        /// <summary>
        /// Checks a room for availability and then books it if it is available in the given period
        /// </summary>
        /// <param name="roomId"></param>
        /// <param name="from"></param>
        /// <param name="to"></param>
        int BookRoom(BookingRequest req);
    }
}
