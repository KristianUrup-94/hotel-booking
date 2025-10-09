using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Models
{
    /// <summary>
    /// A request for getting available rooms in given period
    /// </summary>
    public class AvailableRoomsRequest
    {
        public AvailableRoomsRequest(DateTimeOffset from, DateTimeOffset to)
        {
            From = from;
            To = to;
        }
        public AvailableRoomsRequest()
        {

        }
        /// <summary>
        /// The start of the period
        /// </summary>
        public DateTimeOffset From { get; set; }
        /// <summary>
        /// The end of the period
        /// </summary>
        public DateTimeOffset To { get; set; }
        
    }
}
