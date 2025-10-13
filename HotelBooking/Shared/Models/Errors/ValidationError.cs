using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Models.Errors
{
    /// <summary>
    /// Error given for validation
    /// </summary>
    public static class ValidationError
    {
        /// <summary>
        /// Validation is null
        /// </summary>
        public const string IsNull = "is null";
        /// <summary>
        /// Validation is empty
        /// </summary>
        public const string IsEmpty = "is empty";
        /// <summary>
        /// Validation is only whitespace
        /// </summary>
        public const string IsOnlyWhitespace = "is only whitespace";
        /// <summary>
        /// Template for validation error
        /// <para>First string is the property name</para>
        /// <para>Second is the validation of the property name (IsNull or IsEmpty)</para>
        /// </summary>
        public const string Template = "The property named {0} {1}";
    }
}
