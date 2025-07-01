using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bookify.Application.Exceptions
{
    public sealed class ValidationException(IEnumerable<ValidationError> errors) : Exception
    {
        public IEnumerable<ValidationError> Errors { get; } = errors;
    }
}
