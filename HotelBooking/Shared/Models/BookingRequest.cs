using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Models
{
    public class BookingRequest
    {
        /// <summary>
        /// Unique id of the room which the user want to book
        /// </summary>
        public int RoomId { get; set; }

        /// <summary>
        /// Date from when the booking should start
        /// </summary>
        public DateTimeOffset From { get; set; }

        /// <summary>
        /// Date to when the booking should end
        /// </summary>
        public DateTimeOffset To { get; set; }
    }
}
