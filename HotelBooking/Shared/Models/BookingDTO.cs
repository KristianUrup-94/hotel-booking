using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Models
{
    /// <summary>
    /// Booking of a room
    /// </summary>
    public class BookingDTO
    {
        /// <summary>
        /// Number of the booking
        /// </summary>
        public int? BookingNo { get; set; }
        /// <summary>
        /// DateTime for when the booking starts
        /// </summary>
        public DateTimeOffset From { get; set; }
        /// <summary>
        /// DateTime for when the booking ends
        /// </summary>
        public DateTimeOffset To { get; set; }
        /// <summary>
        /// Comments given during booking
        /// </summary>
        public string? Comments { get; set; }
        /// <summary>
        /// Id of the room booked
        /// </summary>
        public int RoomId { get; set; }
    }
}
