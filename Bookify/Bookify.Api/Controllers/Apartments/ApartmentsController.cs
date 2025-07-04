﻿using Bookify.Application.Apartments.SearchApartments;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Bookify.Api.Controllers.Apartments;


[ApiController]
[Route("api/apartments")]
public class ApartmentsController(ISender sender) : ControllerBase
{
    private readonly ISender _sender = sender;

    [HttpGet]
    public async Task<IActionResult> SearchApartments(DateOnly startDate, DateOnly endDate, CancellationToken cancellationToken)
    {
        var query = new SearchApartmentsQuery(startDate, endDate);

        Domain.Abstractions.Result<IReadOnlyList<ApartmentResponse>> result = await _sender.Send(query, cancellationToken);

        return Ok(result.Value);

    }
}
