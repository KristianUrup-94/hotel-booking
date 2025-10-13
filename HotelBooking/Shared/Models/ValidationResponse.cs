using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Models
{
    /// <summary>
    /// Response which are given by the ValidateService
    /// </summary>
    public class ValidationResponse
    {
        /// <summary>
        /// Gives the result of the validation
        /// <para>true if validation was correct</para>
        /// <para>false if validation was incorrect</para>
        /// <para>if false error is not null</para>
        /// </summary>
        public bool Result { get; set; }
        /// <summary>
        /// Errors given if result is false
        /// </summary>
        public List<string>? Errors { get; set; }
    }
}
