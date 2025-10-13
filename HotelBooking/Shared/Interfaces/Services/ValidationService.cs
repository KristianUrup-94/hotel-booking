using Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Interfaces.Services
{
    public class ValidationService : IValidationService
    {
        public ValidationResponse ValidateNoNullOrEmpty<T>(T model, List<string> propNames)
        {
            throw new NotImplementedException();
        }
    }
}
