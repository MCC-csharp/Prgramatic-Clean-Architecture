using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bookify.Infrastructure.Authentication
{
    public sealed  class AuthenticationOptions
    {
        public string Audience { get; set; } = string.Empty;

        public string MetadataUrl { get; set; } = string.Empty;

        public bool RequireHttpsMetadata { get; init}

        public string Issuer { get; set; } = string.Empty;
    }
}
