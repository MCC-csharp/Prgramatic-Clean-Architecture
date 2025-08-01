﻿using Bookify.Application.Abstractions.Data;
using Bookify.Application.Abstractions.Messaging;
using Bookify.Domain.Abstractions;
using Bookify.Domain.Bookings;
using Dapper;

namespace Bookify.Application.Apartments.SearchApartments;

internal sealed class SearchApartmentsQueryHandler(ISqlConnectionFactory sqlConnectionFactory) : IQueryHandler<SearchApartmentsQuery, IReadOnlyList<ApartmentResponse>>
{
    private readonly int[] ActiveBookingStatuses = {
        (int)BookingStatus.PendingReservation,
        (int)BookingStatus.Confirmed,
        (int)BookingStatus.Completed};

    private readonly ISqlConnectionFactory _sqlConnectionFactory = sqlConnectionFactory;

    public async Task<Result<IReadOnlyList<ApartmentResponse>>> Handle(SearchApartmentsQuery request, CancellationToken cancellationToken)
    {
        if (request.StartDate > request.EndDate)
        {
            return new List<ApartmentResponse>();
        }

        using System.Data.IDbConnection connection = _sqlConnectionFactory.CreateConnection();

        const string sql = """
                SELECT
                    a.id AS Id,
                    a.name AS Name,
                    a.description AS Description,
                    a.price_amount AS Price,
                    a.price_currency AS Currency,
                    a.address_country AS Country,
                    a.address_state AS State,
                    a.address_zip_code AS ZipCode,
                    a.address_city AS City,
                    a.address_street AS Street
                FROM apartments AS a
                WHERE NOT EXISTS (
                    SELECT 1
                    FROM bookings AS b
                    WHERE 
                        b.apartment_id = a.id AND
                        b.duration_start <= @EndDate AND
                        b.duration_end >= @StartDate AND
                        b.status = ANY(@ActiveBookingStatuses)
                )
                """;

        IEnumerable<ApartmentResponse> apartments = await connection
            .QueryAsync<ApartmentResponse, AddressResponse, ApartmentResponse>(
                sql,
                (apartment, address) => new ApartmentResponse
                {
                    Id = apartment.Id,
                    Name = apartment.Name,
                    Description = apartment.Description,
                    Price = apartment.Price,
                    Currency = apartment.Currency,
                    Address = new AddressResponse
                    {
                        Country = address.Country,
                        City = address.City,
                        Street = address.Street,
                        ZipCode = address.ZipCode,
                        State = address.State
                    }
                },
                new
                {
                    request.StartDate,
                    request.EndDate,
                    ActiveBookingStatuses
                },
                splitOn: "Street").ConfigureAwait(false);

        return apartments.ToList();
    }
}
