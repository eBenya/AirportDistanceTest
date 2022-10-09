using AirportApiTest.Models.Records;
using AirportApiTest.Services.PortServices;

namespace AirportApiTest.Extensions;

public static class ControllerHelpers
{
    public static Task<(Location, Location)> GetTwoAirportsLocation(this IPortInfoService portInfoService,
        string codeIATA1,
        string codeIATA2,
        CancellationToken ctk)
    {
        var ap1 = portInfoService.GetAirportLocationAsync(codeIATA1.ToUpper(), ctk);
        var ap2 = portInfoService.GetAirportLocationAsync(codeIATA2.ToUpper(), ctk);

        return Task.WhenAll(ap1, ap2)
            .ContinueWith(_ => (portLocation1: ap1.Result, portLocation2: ap2.Result), ctk);
    }
}