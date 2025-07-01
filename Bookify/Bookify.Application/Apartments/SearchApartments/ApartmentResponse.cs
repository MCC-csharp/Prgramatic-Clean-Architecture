using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bookify.Application.Apartments.SearchApartments
{
    public sealed class ApartmentResponse
    {
        public Guid Id { get; init; }
        public string Name { get; init; }
        public string Description { get; init; }
        public AddressResponse Address { get; init; }
        public decimal Price { get; init; }
        public string Currency { get; init; }
    }
}
