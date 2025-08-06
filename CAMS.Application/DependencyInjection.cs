using CAMS.Application.Factories;
using CAMS.Application.Interfaces;
using CAMS.Application.Services;
using Microsoft.Extensions.DependencyInjection;

namespace CAMS.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddCamsApplicationServices(this IServiceCollection services)
    {
        services.AddTransient<IVehicleTypeCreator, HatchbackTypeCreator>();
        services.AddTransient<IVehicleTypeCreator, SedanTypeCreator>();
        services.AddTransient<IVehicleTypeCreator, SuvTypeCreator>();
        services.AddTransient<IVehicleTypeCreator, TruckTypeCreator>();
        services.AddTransient<IVehicleFactory, VehicleFactory>();

        services.AddScoped<IVehicleService, VehicleService>();
        services.AddScoped<IAuctionOrchestrator, AuctionOrchestrator>();

        return services;
    }
}