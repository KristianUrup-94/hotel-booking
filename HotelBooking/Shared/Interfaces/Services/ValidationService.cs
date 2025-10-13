using Shared.Models;
using Shared.Models.Errors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Shared.Interfaces.Services
{
    /// <summary>
    /// Service for validating objects
    /// </summary>
    public class ValidationService : IValidationService
    {
        /// <summary>
        /// Validates the given properties for null, empty or whitespace
        /// </summary>
        /// <typeparam name="T">The type</typeparam>
        /// <param name="model">The model which contains the properties</param>
        /// <param name="propNames">Names of the properties</param>
        /// <returns>Response which gives the errors if any</returns>
        public ValidationResponse ValidateNullEmptyOrWhitespace<T>(T model, List<string> propNames)
        {
            List<string>? errors = new List<string>();
            foreach (var prop in propNames) 
            {
                var value = model?.GetType()?.GetProperty(prop)?.GetValue(model);
                var error = GetError(value, prop);
                if (error != null)
                {
                    errors.Add(error);
                }
            }
            if (errors.Count == 0) 
            {
                errors = null;
            }
            return new ValidationResponse { Errors = errors, Result = errors == null };
        }
        private string? GetError(object value, string prop)
        {
            if (value == null)
            {
                return string.Format(string.Format(ValidationError.Template, prop, ValidationError.IsNull));
            }
            if(value.GetType() != typeof(string))
            {
                return null;
            }
            if (string.IsNullOrEmpty(value?.ToString()))
            {
                return string.Format(string.Format(ValidationError.Template, prop, ValidationError.IsEmpty));
            }
            if (string.IsNullOrWhiteSpace(value?.ToString()))
            {
                return string.Format(string.Format(ValidationError.Template, prop, ValidationError.IsOnlyWhitespace));
            }
            return null;
        }
    }
}
