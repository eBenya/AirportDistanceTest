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
        var twoAirportsLocationTuple = await _portInfoService.GetTwoAirportsLocation(codeIATA1, codeIATA2, ctk);
        var p1 = twoAirportsLocationTuple.Item1.Adapt<Point>();
        var p2 = twoAirportsLocationTuple.Item2.Adapt<Point>();

        double result = DistanceService.Calculate(p1, p2);

        return Ok(new
        {
            distance = result,
            userMessage = $"The distance between {codeIATA1} and {codeIATA2} objects is {result} mile"
        });
    }
}