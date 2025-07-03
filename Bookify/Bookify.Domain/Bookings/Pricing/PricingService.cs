using Bookify.Domain.Abstractions.PriceCalculation;
using Bookify.Domain.Apartments;
using Bookify.Domain.DomainShared;

namespace Bookify.Domain.Bookings.Pricing;


public class PricingService : IPricingService
{
    private readonly AmenityPricingStrategy _pricingStrategy;

    public PricingService(AmenityPricingStrategy pricingStrategy)
    {
        _pricingStrategy = pricingStrategy;
    }

    public PricingDetails CalculatePrice(Apartment apartment, DateRange period)
    {
        ArgumentNullException.ThrowIfNull(apartment);
        ArgumentNullException.ThrowIfNull(period);

        Currency currency = apartment.Price.Currency;

        var priceForPeriod = new Money(
            apartment.Price.Amount * period.LengthInDays,
            currency);

        decimal percentageUpCharge = apartment.Amenities
            .Sum(amenity => _pricingStrategy(amenity));


        Money amenitiesUpCharge = percentageUpCharge > 0
            ? new Money(priceForPeriod.Amount * percentageUpCharge, currency)
            : Money.Zero(currency);

        var totalPrice = Money.Zero(currency);

        totalPrice += priceForPeriod;

        if (!apartment.CleaningFee.IsZero())
        {
            totalPrice += apartment.CleaningFee;
        }

        totalPrice += amenitiesUpCharge;

        return new PricingDetails(priceForPeriod, apartment.CleaningFee, amenitiesUpCharge, totalPrice);
    }
}
