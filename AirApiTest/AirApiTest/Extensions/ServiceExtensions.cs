using AirportApiTest.Services.PortServices;

namespace AirportApiTest.Extensions;

public static class ServiceExtensions
{
    public static IServiceCollection AddInternalServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<IPortInfoService, PortInfoService>();

        services.AddHttpClient<IPortInfoService, PortInfoService>("AirportInfoService")
            .ConfigureHttpClient(client =>
            {
                client.BaseAddress = new Uri(configuration.GetSection("AirportInfoService:BaseUrl").Value);
            });

        return services;
    }
}