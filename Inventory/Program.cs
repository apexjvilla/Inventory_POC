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
        services.AddSingleton<IInventoryService1, InventoryService>();
        services.AddSingleton<IInventoryValidations, InventoryValidations>();
        services.AddSingleton<MenuManager>();

        var serviceProvider = services.BuildServiceProvider();

        // Initialize sample data
        var productService = serviceProvider.GetRequiredService<IProductService>();
        var warehouseService = serviceProvider.GetRequiredService<IWarehouseService>();

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

        Warehouse newWarehouse = new Warehouse
        {
            Name = "Warehouse A",
            Location = "Location A"
        };
        warehouseService.Add(newWarehouse);

        Console.WriteLine("Inventory system initialized with sample data.");
        Console.WriteLine("Press any key to continue...");
        Console.ReadKey();

        // Launch menu
        var menuManager = serviceProvider.GetRequiredService<MenuManager>();
        menuManager.Run();
    }
}