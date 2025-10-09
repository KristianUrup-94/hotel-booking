using Rooms.Entity;

namespace Rooms.Services.Interfaces
{
    /// <summary>
    /// Manager for Rooms
    /// </summary>
    public interface IRoomManager
    {
        /// <summary>
        /// Getting all of the rooms available from the given period
        /// </summary>
        /// <param name="from">The period start</param>
        /// <param name="to">The period end</param>
        /// <returns></returns>
        public List<Room> GettingAllAvailableRooms(DateTimeOffset from, DateTimeOffset to);
    }
}
