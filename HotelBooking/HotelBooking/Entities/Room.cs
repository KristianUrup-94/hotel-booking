using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Rooms.Entity
{
    /// <summary>
    /// A Room which can be booked
    /// </summary>
    public class Room
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        /// <summary>
        /// Unique id of the room
        /// </summary>
        public int Id { get; set; }
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
