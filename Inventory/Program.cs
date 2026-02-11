using Inventory.Domain.Entities;
using Inventory.Domain.Enums;
using Inventory.Extensions;
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
        services.AddSingleton<IInventoryValidations, InventoryValidations>();

        var serviceProvider = services.BuildServiceProvider();

        // Create services
                var productService = serviceProvider.GetRequiredService<IProductService>();
        var warehouseService = serviceProvider.GetRequiredService<IWarehouseService>();
        var movementService = serviceProvider.GetRequiredService<IMovementService>();

        // Example usage
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

        var products = productService.GetAll();
        Console.WriteLine("Products:");
        Console.WriteLine(products.ToJson());

        var warehouses = warehouseService.GetAll();
        Console.WriteLine("Warehouses:");
        Console.WriteLine(warehouses.ToJson());

        var movements = movementService.GetAll();
        Console.WriteLine("Movements:");
        Console.WriteLine(movements.ToJson());

        Console.ReadKey();
    }
}