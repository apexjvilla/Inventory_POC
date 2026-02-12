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
        services.AddSingleton<IMovementRepository, InMemoryMovementRepository>();

        // Register services
        services.AddSingleton<IProductService, ProductService>();
        services.AddSingleton<IWarehouseService, WarehouseService>();
        services.AddSingleton<IInventoryService, InventoryService>();
        services.AddSingleton<IInventoryService1, InventoryService>();
        services.AddSingleton<IInventoryValidations, InventoryValidations>();

        // Register MenuManager
        services.AddSingleton<MenuManager>();

        var serviceProvider = services.BuildServiceProvider();

        // Initialize sample data
        InitializeSampleData(serviceProvider);

        // Start the interactive menu
        var menuManager = serviceProvider.GetRequiredService<MenuManager>();
        menuManager.Run();
    }

    private static void InitializeSampleData(ServiceProvider serviceProvider)
    {
        var productService = serviceProvider.GetRequiredService<IProductService>();
        var warehouseService = serviceProvider.GetRequiredService<IWarehouseService>();

        // Create sample products
        Product newProduct = new Product
        {
            Name = "Product A",
            Description = "Description of Product A"
        };
        productService.Add(newProduct);

        Product newProduct1 = new Product
        {
            Name = "Product B",
            Description = "Description of Product B"
        };
        productService.Add(newProduct1);

        // Create sample warehouse
        Warehouse newWarehouse = new Warehouse
        {
            Name = "Warehouse A",
            Location = "Location A"
        };
        warehouseService.Add(newWarehouse);
    }
}