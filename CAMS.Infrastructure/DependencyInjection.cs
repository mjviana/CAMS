using CAMS.Application.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace CAMS.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddCamsInfrastructureServices(this IServiceCollection services)
    {
        services.AddSingleton<IVehicleRepository, InMemoryVehicleRepository>();
        services.AddSingleton<IAuctionRepository, InMemoryAuctionRepository>();

        return services;
    }
}