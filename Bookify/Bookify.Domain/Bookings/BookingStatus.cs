namespace Bookify.Domain.Bookings;

public enum BookingStatus
{
    None = 0,
    PendingReservation = 1,
    Confirmed = 2,
    Rejected = 3,
    Cancelled = 4,
    Completed = 5
}