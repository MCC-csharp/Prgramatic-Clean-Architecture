using Bogus;
using Bookify.Application.Abstractions.Data;
using Bookify.Domain.Apartments;
using Dapper;

namespace Bookify.Api.Extensions
{
    public static class SeedDataExtensions
    {
        public static void SeedData(this IApplicationBuilder app)
        {
            using var scope = app.ApplicationServices.CreateScope();

            var sqlConnectionsFactory = scope.ServiceProvider.GetRequiredService<ISqlConnectionFactory>();
            using var connection = sqlConnectionsFactory.CreateConnection();

            var faker = new Faker();

            List<object> apartments = new List<object>();
            for (var i = 0; i < 100; i++)
            {
                apartments.Add(new
                {
                    Id = Guid.NewGuid(),
                    Name = faker.Company.CompanyName(),
                    Description = faker.Lorem.Paragraph(),
                    Country = faker.Address.Country(),
                    City = faker.Address.City(),
                    Street = faker.Address.StreetName(),
                    PostalCode = faker.Address.ZipCode(),
                    State = faker.Address.State(),
                    PriceAmount = faker.Random.Decimal(50, 1000),
                    PriceCurrency = "EURO",
                    CleaningFeeAmount = faker.Random.Decimal(25, 200),
                    CleanFeeCurrency = "EURO",
                    Amenities = new List<int> { (int)Amenity.Parking, (int)Amenity.MountainView },
                    LastBookedOn = DateTime.MinValue
                });
            }

            const string sql = """  
                       INSERT INTO public.apartments  
                       (Id, "name", description, address_country, address_city, address_street, address_zip_code, address_state,  
                       price_amount, price_currency, cleaning_fee_amount, cleaning_fee_currency, amenities, last_booked_on_utc)  
                       VALUES(@Id, @Name, @Description, @Country, @City, @Street, @PostalCode, @State, @PriceAmount, @PriceCurrency, @CleaningFeeAmount, @CleanFeeCurrency, @Amenities, @LastBookedOn);  
                   """;
            connection.Execute(sql, apartments);
        }
    }
}
