using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Models
{
    /// <summary>
    /// Data transfer object for Room
    /// </summary>
    public class RoomDTO
    {
        /// <summary>
        /// Unique id of the room
        /// </summary>
        public int? Id { get; set; }
        /// <summary>
        /// The name of the 
        /// <para>(Room 101)</para>
        /// </summary>
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Description of the Room 
        /// </summary>
        public string? Description { get; set; }
    }
}
