﻿using Bookify.Domain.Apartments;
using Bookify.Domain.Bookings;
using Microsoft.EntityFrameworkCore;

namespace Bookify.Infrastructure.Repositories;

internal sealed class BookingRepository(ApplicationDBContext dbContext) : Repository<Booking>(dbContext), IBookingRepository
{
    private static readonly BookingStatus[] ActiveBookingStatuses =
    {
        BookingStatus.PendingReservation,
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
