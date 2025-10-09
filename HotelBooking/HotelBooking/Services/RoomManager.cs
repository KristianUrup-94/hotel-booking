using Rooms.Entity;
using Rooms.Services.Interfaces;
using Shared.Interfaces;
using Shared.Interfaces.BaseClasses;
using Shared.Models;

namespace Rooms.Services
{
    /// <summary>
    /// Manager for Rooms
    /// </summary>
    public class RoomManager : IRoomManager
    {
        private readonly IRepository<Room> _repo;

        /// <summary>
        /// Constructor for dependency injection
        /// </summary>
        /// <param name="repo"></param>
        public RoomManager(IRepository<Room> repo) 
        {
            _repo = repo;
        }
        /// <summary>
        /// Getting all of the rooms available from the given period
        /// </summary>
        /// <param name="from">The period start</param>
        /// <param name="to">The period end</param>
        /// <returns></returns>
        public List<Room> GettingAllAvailableRooms(DateTimeOffset from, DateTimeOffset to)
        {
            BaseConnection bookingConnection = new("https://localhost:44366/api/");

            List<int> response = bookingConnection.PostAsync<List<int>>("bookings/GetAllBookedRoomIds", new AvailableRoomsRequest(from, to))
                .GetAwaiter()
                .GetResult();

            List<Room> result = _repo.Query().Where(x => response.Contains(x.Id) == false).ToList();


            return result;
        }
    }
}
