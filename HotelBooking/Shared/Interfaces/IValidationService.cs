using Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Interfaces
{
    /// <summary>
    /// Service for validating objects
    /// </summary>
    public interface IValidationService
    {
        /// <summary>
        /// Validates the given properties for null, empty or whitespace
        /// </summary>
        /// <typeparam name="T">The type</typeparam>
        /// <param name="model">The model which contains the properties</param>
        /// <param name="propNames">Names of the properties</param>
        /// <returns>Response which gives the errors if any</returns>
        ValidationResponse ValidateNullEmptyOrWhitespace<T>(T model, List<string> propNames);
    }
}
