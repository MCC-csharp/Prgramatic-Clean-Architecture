using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bookify.Application.Bookings.GetBooking
{
    public sealed class BookingResponse
    {
        public Guid Id { get; init; }
        public Guid ApartmentId { get; init; }
        public Guid UserId { get; init; }
        public int Status { get; init; }
        public decimal PriceAmount { get; init; }
        public required string PriceCurrency { get; init; }
        public decimal CleaningFeeAmount { get; init; }
        public required string CleaningFeeCurrency { get; init; }
        public decimal AmenitiesUpChargeAmount { get; init; }
        public required string AmenitiesUpChargeCurrency { get; init; }
        public decimal TotalPriceAmount { get; init; }
        public DateTime StartDate { get; init; }
        public DateTime EndDate { get; init; }

    }
}
