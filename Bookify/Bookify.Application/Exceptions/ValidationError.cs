using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bookify.Application.Exceptions
{
    public sealed record ValidationError(string PropertyName, string ErrorMessage);
}
