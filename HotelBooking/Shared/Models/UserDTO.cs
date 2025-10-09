using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Models
{
    /// <summary>
    /// A User for the booking
    /// </summary>
    public class UserDTO
    {
        /// <summary>
        /// Unique id of the User
        /// </summary>
        public int? Id { get; set; }
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
