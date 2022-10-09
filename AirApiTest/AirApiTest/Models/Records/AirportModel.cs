using System.Text.Json.Serialization;

namespace AirportApiTest.Models.Records;

public record Location(
    [property: JsonPropertyName("lon")] double X,
    [property: JsonPropertyName("lat")] double Y
);

public record Root(
    [property: JsonPropertyName("country")] string Country,
    [property: JsonPropertyName("city_iata")] string CityIata,
    [property: JsonPropertyName("iata")] string Iata,
    [property: JsonPropertyName("city")] string City,
    [property: JsonPropertyName("timezone_region_name")] string TimezoneRegionName,
    [property: JsonPropertyName("country_iata")] string CountryIata,
    [property: JsonPropertyName("rating")] int Rating,
    [property: JsonPropertyName("name")] string Name,
    [property: JsonPropertyName("location")] Location Location,
    [property: JsonPropertyName("type")] string Type,
    [property: JsonPropertyName("hubs")] int Hubs
);