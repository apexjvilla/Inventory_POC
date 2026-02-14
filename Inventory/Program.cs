using Inventory.ConsoleMenu;
using Inventory.Domain.Entities;
using Inventory.Repository.Abstractions;
using Inventory.Repository.Implementations;
using Inventory.Services.Abstractions;
using Inventory.Services.Implementations;
using Microsoft.Extensions.DependencyInjection;

internal class Program
{
    private static void Main(string[] args)
    {
        // Configure DI
        var services = new ServiceCollection();

        // Register repositories
        services.AddSingleton<IProductRepository, InMemoryProductRepository>();
        services.AddSingleton<IWarehouseRepository, InMemoryWarehouseRepository>();
        services.AddSingleton<IMovementWriteRepository, InMemoryMovementRepository>();
        services.AddSingleton<IMovementReadRepository, InMemoryMovementRepository>();

        // Register services
        services.AddSingleton<IProductService, ProductService>();
        services.AddSingleton<IWarehouseService, WarehouseService>();
        services.AddSingleton<IInventoryService, InventoryService>();
        services.AddSingleton<IInventoryValidationsService, InventoryValidationsService>();

        // Register MenuManager
        services.AddSingleton<MenuManager>();

        var serviceProvider = services.BuildServiceProvider();

        // Seed data
        var productService = serviceProvider.GetRequiredService<IProductService>();
        var warehouseService = serviceProvider.GetRequiredService<IWarehouseService>();

        SeedData.Initialize(productService, warehouseService);

        // Start the interactive menu
        var menuManager = serviceProvider.GetRequiredService<MenuManager>();
        menuManager.Run();
    }
    
}