using System.Text.Json;
using AirportApiTest.Models.Records;

namespace AirportApiTest.Services.PortServices;

public class PortInfoService : IPortInfoService
{
    private readonly HttpClient _httpClient;

    public PortInfoService(HttpClient httpClient)
    {
        _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
    }

    public async Task<Location> GetAirportLocationAsync(string iataCode, CancellationToken ctk)
    {
        var airportContentString = await GetResponseContentAsStringAsync(iataCode, ctk);

        var airportInfo = JsonSerializer.Deserialize<Root>(airportContentString);

        //var api = await JsonSerializer.DeserializeAsync<AirportModel.Root>(
        //    await response.Content.ReadAsStreamAsync(ctk), cancellationToken: ctk);

        return airportInfo.Location;
    }

    private async Task<string> GetResponseContentAsStringAsync(string requestUrl, CancellationToken ctk)
    {
        var response = await _httpClient.GetAsync(requestUrl, ctk);

        return await response.Content.ReadAsStringAsync(ctk);
    }
}