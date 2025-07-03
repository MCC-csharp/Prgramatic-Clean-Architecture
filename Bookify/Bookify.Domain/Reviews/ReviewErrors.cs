using Bookify.Domain.Abstractions;

namespace Bookify.Domain.Reviews;

public static class ReviewErrors
{
    public static readonly DomainError NotEligible = new(
        "Review.NotEligible",
        "The review is not eligible because the booking is not yet completed");
}