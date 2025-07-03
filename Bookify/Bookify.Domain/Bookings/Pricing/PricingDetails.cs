using Bookify.Domain.DomainShared;

namespace Bookify.Domain.Bookings.Pricing;

public record PricingDetails(
    Money PriceForPeriod,
    Money CleaningFee,
    Money AmenitiesUpCharge,
    Money TotalPrice);