using AirportApiTest.Extensions;
using AirportApiTest.Filters;
using AirportApiTest.Services;
using AirportApiTest.Services.PortServices;
using Mapster;
using Microsoft.AspNetCore.Mvc;

namespace AirportApiTest.Controllers;

[ApiController]
[Route("api/[controller]/[action]")]
public class AirportController : Controller
{
    private readonly ILogger<AirportController> _logger;
    private readonly IPortInfoService _portInfoService;

    public AirportController(ILogger<AirportController> logger, IPortInfoService portInfoService)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _portInfoService = portInfoService ?? throw new ArgumentNullException(nameof(portInfoService));
    }

    [CodeIATAFilter]
    [HttpGet("distance", Name = "GetDistance")]
    public async Task<IActionResult> CalculateDistance([FromQuery] string codeIATA1, [FromQuery] string codeIATA2,
        CancellationToken ctk)
    {
        const DistanceService.DistanceType distanceType = DistanceService.DistanceType.Miles;

        var twoAirportsLocationTuple = await _portInfoService.GetTwoAirportsLocation(codeIATA1, codeIATA2, ctk);
        var location1 = twoAirportsLocationTuple.Item1;
        var location2 = twoAirportsLocationTuple.Item2;

        var result = DistanceService.CalculateTwoPointGeoDistanceInMile(location1.Adapt<Point>(), location2.Adapt<Point>(), distanceType);

        return Ok(new
        {
            distance = result,
            userMessage = $"The distance between {codeIATA1} and {codeIATA2} objects is {result} {distanceType}",
            IATA1 = new {
                Name = codeIATA1,
                Location = location1
            },
            IATA2 = new
            {
                Name = codeIATA2,
                Location = location2
            },
        });
    }
}