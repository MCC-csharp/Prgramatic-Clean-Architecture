using Bookify.Domain.Apartments;
using Bookify.Domain.Bookings;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bookify.Infrastructure.Repositories
{
    internal sealed class BookingRepository : Repository<Booking>, IBookingRepository
    {
        public BookingRepository(ApplicationDBContext dbContext) : base(dbContext)
        {
        }

        private static readonly BookingStatus[] ActiveBookingStatuses =
        {
            BookingStatus.Reserved,
            BookingStatus.Confirmed,
            BookingStatus.Completed
        };

        public async Task<bool> IsOverlappingAsync(Apartment apartment, DateRange duration, CancellationToken cancellationToken = default)
        => await DBContext
            .Set<Booking>()
            .AnyAsync(
            booking =>
            booking.ApartmentId == apartment.Id &&
            booking.Duration.Start <= duration.End &&
            booking.Duration.End >= duration.Start &&
            ActiveBookingStatuses.Contains(booking.Status),
            cancellationToken);
            
        
    }
}
