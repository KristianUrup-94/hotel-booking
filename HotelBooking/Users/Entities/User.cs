using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Users.Entity
{
    /// <summary>
    /// A User for the booking
    /// </summary>
    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        /// <summary>
        /// Unique id of the User
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// The first name of the user
        /// </summary>
        public string FirstName { get; set; } = string.Empty;
        /// <summary>
        /// The last name of the user
        /// </summary>
        public string LastName { get; set; } = string.Empty;
        /// <summary>
        /// The address for the user
        /// </summary>
        public string Address { get; set; } = string.Empty;
    }
}
