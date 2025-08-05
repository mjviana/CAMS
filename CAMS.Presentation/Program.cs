// See https://aka.ms/new-console-template for more information

using CAMS.Application;
using CAMS.Application.Dtos;
using CAMS.Application.Interfaces;
using CAMS.Domain.Enums;
using CAMS.Infrastructure;
using Microsoft.Extensions.DependencyInjection;

var services = new ServiceCollection();
services
    .AddCamsApplicationServices()
    .AddCamsInfrastructureServices();

var serviceProvider = services.BuildServiceProvider();

// Get Services from DI container
var vehicleService = serviceProvider.GetRequiredService<IVehicleService>();
var auctionService = serviceProvider.GetRequiredService<IAuctionOrchestrator>();

var vehicleDto = new CreateVehicleDto(
    Type: VehicleType.Suv,
    Manufacturer: "foo",
    Model: "bar",
    Year: 2023,
    StartingBid: 15000m,
    NumberOfDoors: null,
    NumberOfSeats: 5,
    LoadCapacity: null
);

await vehicleService.CreateVehicleAsync(vehicleDto);
Console.WriteLine("Vehicle added to inventory");

var allVehicles = await vehicleService.GetAllVehiclesAsync();
var createdVehicle = allVehicles.Last();

await auctionService.StartAuctionAsync(createdVehicle.Id);
Console.WriteLine("Auction started");

var openAuctions = await auctionService.GetAllOpenAuctionsAsync();

var currentAuction = openAuctions.FirstOrDefault();

Console.WriteLine($"Auction bid before bid action: {currentAuction.CurrentBid?.Value}");

await auctionService.PlaceBidAsync(currentAuction.Id, new PlaceBidDto("user 01", 16000));

Console.WriteLine($"Auction bid after bid action: {currentAuction.CurrentBid.Value}");

await auctionService.CloseAuctionAsync(currentAuction.Id);
Console.WriteLine("Auction closed");

Console.WriteLine("\n Demo completed. Press any key to exit...");
Console.ReadKey();