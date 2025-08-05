using CAMS.Application.Interfaces;
using CAMS.Application.Services;
using Microsoft.Extensions.DependencyInjection;

namespace CAMS.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddCamsApplicationServices(this IServiceCollection services)
    {
        services.AddScoped<IVehicleService, VehicleService>();
        services.AddScoped<IAuctionOrchestrator, AuctionOrchestrator>();

        return services;
    }
}