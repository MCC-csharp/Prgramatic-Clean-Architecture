using Bookify.Domain.Abstractions;

namespace Bookify.Domain.Bookings;

public static class BookingErrors
{
    public static readonly DomainError NotFound = new(
        "Booking.Found",
        "The booking with the specified identifier was not found");

    public static readonly DomainError Overlap = new(
        "Booking.Overlap",
        "The current booking is overlapping with an existing one");

    public static readonly DomainError NotReserved = new(
        "Booking.NotReserved",
        "The booking is not pending");

    public static readonly DomainError NotConfirmed = new(
        "Booking.NotReserved",
        "The booking is not confirmed");

    public static readonly DomainError AlreadyStarted = new(
        "Booking.AlreadyStarted",
        "The booking has already started");
}