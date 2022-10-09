using AirportApiTest.Models.Records;

namespace AirportApiTest.Services.PortServices;

public interface IPortInfoService
{
    Task<Location> GetAirportLocationAsync(string iataCode, CancellationToken ctk);
}