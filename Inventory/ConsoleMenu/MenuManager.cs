using Inventory.Domain.Entities;
using Inventory.Extensions;
using Inventory.Services.Abstractions;

namespace Inventory.ConsoleMenu
{
    public class MenuManager
    {
        private readonly IProductService _productService;
        private readonly IWarehouseService _warehouseService;
        private readonly IInventoryService1 _inventoryService;

        public MenuManager(IProductService productService, IWarehouseService warehouseService, IInventoryService1 inventoryService)
        {
            _productService = productService;
            _warehouseService = warehouseService;
            _inventoryService = inventoryService;
        }

        public void Run()
        {
            bool exit = false;
            while (!exit)
            {
                Console.Clear();
                ShowMainMenu();
                var choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        ProductManagementMenu();
                        break;
                    case "2":
                        WarehouseManagementMenu();
                        break;
                    case "3":
                        InventoryOperationsMenu();
                        break;
                    case "4":
                        exit = true;
                        break;
                    default:
                        Console.WriteLine("✗ Invalid option. Please try again.");
                        Console.WriteLine("Press any key to continue...");
                        Console.ReadKey();
                        break;
                }
            }
        }

        private void ShowMainMenu()
        {
            Console.WriteLine("╔════════════════════════════════════════╗");
            Console.WriteLine("║     INVENTORY MANAGEMENT SYSTEM        ║");
            Console.WriteLine("╚════════════════════════════════════════╝");
            Console.WriteLine();
            Console.WriteLine("1. Product Management");
            Console.WriteLine("2. Warehouse Management");
            Console.WriteLine("3. Inventory Operations");
            Console.WriteLine("4. Exit");
            Console.WriteLine();
            Console.Write("Select an option: ");
        }

        #region Product Management

        private void ProductManagementMenu()
        {
            bool back = false;
            while (!back)
            {
                Console.Clear();
                Console.WriteLine("╔════════════════════════════════════════╗");
                Console.WriteLine("║        PRODUCT MANAGEMENT              ║");
                Console.WriteLine("╚════════════════════════════════════════╝");
                Console.WriteLine();
                Console.WriteLine("1. Create Product");
                Console.WriteLine("2. Update Product");
                Console.WriteLine("3. Delete Product");
                Console.WriteLine("4. Get All Products");
                Console.WriteLine("5. Get Product by ID");
                Console.WriteLine("6. Back to Main Menu");
                Console.WriteLine();
                Console.Write("Select an option: ");

                var choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        CreateProduct();
                        break;
                    case "2":
                        UpdateProduct();
                        break;
                    case "3":
                        DeleteProduct();
                        break;
                    case "4":
                        GetAllProducts();
                        break;
                    case "5":
                        GetProductById();
                        break;
                    case "6":
                        back = true;
                        break;
                    default:
                        Console.WriteLine("✗ Invalid option. Please try again.");
                        Console.WriteLine("Press any key to continue...");
                        Console.ReadKey();
                        break;
                }
            }
        }

        private void CreateProduct()
        {
            Console.Clear();
            Console.WriteLine("=== CREATE PRODUCT ===");
            Console.WriteLine();

            try
            {
                Console.Write("Enter Product Name: ");
                var name = Console.ReadLine();

                Console.Write("Enter Product Description: ");
                var description = Console.ReadLine();

                var product = new Product
                {
                    Name = name,
                    Description = description
                };

                _productService.Add(product);
                Console.WriteLine("✓ Product created successfully!");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"✗ Error: {ex.Message}");
            }

            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
        }

        private void UpdateProduct()
        {
            Console.Clear();
            Console.WriteLine("=== UPDATE PRODUCT ===");
            Console.WriteLine();

            try
            {
                Console.Write("Enter Product ID: ");
                if (!int.TryParse(Console.ReadLine(), out int id))
                {
                    Console.WriteLine("✗ Invalid ID. Please enter a numeric value.");
                    Console.WriteLine("Press any key to continue...");
                    Console.ReadKey();
                    return;
                }

                var product = _productService.GetById(id);
                if (product == null)
                {
                    Console.WriteLine($"✗ Product with ID {id} not found.");
                    Console.WriteLine("Press any key to continue...");
                    Console.ReadKey();
                    return;
                }

                Console.Write($"Enter Product Name (current: {product.Name}): ");
                var name = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(name))
                    product.Name = name;

                Console.Write($"Enter Product Description (current: {product.Description}): ");
                var description = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(description))
                    product.Description = description;

                _productService.Update(product);
                Console.WriteLine("✓ Product updated successfully!");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"✗ Error: {ex.Message}");
            }

            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
        }

        private void DeleteProduct()
        {
            Console.Clear();
            Console.WriteLine("=== DELETE PRODUCT ===");
            Console.WriteLine();

            try
            {
                Console.Write("Enter Product ID: ");
                if (!int.TryParse(Console.ReadLine(), out int id))
                {
                    Console.WriteLine("✗ Invalid ID. Please enter a numeric value.");
                    Console.WriteLine("Press any key to continue...");
                    Console.ReadKey();
                    return;
                }

                var product = _productService.GetById(id);
                if (product == null)
                {
                    Console.WriteLine($"✗ Product with ID {id} not found.");
                    Console.WriteLine("Press any key to continue...");
                    Console.ReadKey();
                    return;
                }

                Console.Write($"Are you sure you want to delete product '{product.Name}'? (Y/N): ");
                var confirmation = Console.ReadLine()?.ToUpper();

                if (confirmation == "Y")
                {
                    _productService.Delete(id);
                    Console.WriteLine("✓ Product deleted successfully!");
                }
                else
                {
                    Console.WriteLine("Delete operation cancelled.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"✗ Error: {ex.Message}");
            }

            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
        }

        private void GetAllProducts()
        {
            Console.Clear();
            Console.WriteLine("=== ALL PRODUCTS ===");
            Console.WriteLine();

            try
            {
                var products = _productService.GetAll();
                if (!products.Any())
                {
                    Console.WriteLine("No products found.");
                }
                else
                {
                    Console.WriteLine(products.ToJson());
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"✗ Error: {ex.Message}");
            }

            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
        }

        private void GetProductById()
        {
            Console.Clear();
            Console.WriteLine("=== GET PRODUCT BY ID ===");
            Console.WriteLine();

            try
            {
                Console.Write("Enter Product ID: ");
                if (!int.TryParse(Console.ReadLine(), out int id))
                {
                    Console.WriteLine("✗ Invalid ID. Please enter a numeric value.");
                    Console.WriteLine("Press any key to continue...");
                    Console.ReadKey();
                    return;
                }

                var product = _productService.GetById(id);
                if (product == null)
                {
                    Console.WriteLine($"✗ Product with ID {id} not found.");
                }
                else
                {
                    Console.WriteLine(product.ToJson());
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"✗ Error: {ex.Message}");
            }

            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
        }

        #endregion

        #region Warehouse Management

        private void WarehouseManagementMenu()
        {
            bool back = false;
            while (!back)
            {
                Console.Clear();
                Console.WriteLine("╔════════════════════════════════════════╗");
                Console.WriteLine("║       WAREHOUSE MANAGEMENT             ║");
                Console.WriteLine("╚════════════════════════════════════════╝");
                Console.WriteLine();
                Console.WriteLine("1. Create Warehouse");
                Console.WriteLine("2. Update Warehouse");
                Console.WriteLine("3. Delete Warehouse");
                Console.WriteLine("4. Get All Warehouses");
                Console.WriteLine("5. Get Warehouse by ID");
                Console.WriteLine("6. Back to Main Menu");
                Console.WriteLine();
                Console.Write("Select an option: ");

                var choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        CreateWarehouse();
                        break;
                    case "2":
                        UpdateWarehouse();
                        break;
                    case "3":
                        DeleteWarehouse();
                        break;
                    case "4":
                        GetAllWarehouses();
                        break;
                    case "5":
                        GetWarehouseById();
                        break;
                    case "6":
                        back = true;
                        break;
                    default:
                        Console.WriteLine("✗ Invalid option. Please try again.");
                        Console.WriteLine("Press any key to continue...");
                        Console.ReadKey();
                        break;
                }
            }
        }

        private void CreateWarehouse()
        {
            Console.Clear();
            Console.WriteLine("=== CREATE WAREHOUSE ===");
            Console.WriteLine();

            try
            {
                Console.Write("Enter Warehouse Name: ");
                var name = Console.ReadLine();

                Console.Write("Enter Warehouse Location: ");
                var location = Console.ReadLine();

                var warehouse = new Warehouse
                {
                    Name = name,
                    Location = location
                };

                _warehouseService.Add(warehouse);
                Console.WriteLine("✓ Warehouse created successfully!");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"✗ Error: {ex.Message}");
            }

            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
        }

        private void UpdateWarehouse()
        {
            Console.Clear();
            Console.WriteLine("=== UPDATE WAREHOUSE ===");
            Console.WriteLine();

            try
            {
                Console.Write("Enter Warehouse ID: ");
                if (!int.TryParse(Console.ReadLine(), out int id))
                {
                    Console.WriteLine("✗ Invalid ID. Please enter a numeric value.");
                    Console.WriteLine("Press any key to continue...");
                    Console.ReadKey();
                    return;
                }

                var warehouse = _warehouseService.GetById(id);
                if (warehouse == null)
                {
                    Console.WriteLine($"✗ Warehouse with ID {id} not found.");
                    Console.WriteLine("Press any key to continue...");
                    Console.ReadKey();
                    return;
                }

                Console.Write($"Enter Warehouse Name (current: {warehouse.Name}): ");
                var name = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(name))
                    warehouse.Name = name;

                Console.Write($"Enter Warehouse Location (current: {warehouse.Location}): ");
                var location = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(location))
                    warehouse.Location = location;

                _warehouseService.Update(warehouse);
                Console.WriteLine("✓ Warehouse updated successfully!");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"✗ Error: {ex.Message}");
            }

            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
        }

        private void DeleteWarehouse()
        {
            Console.Clear();
            Console.WriteLine("=== DELETE WAREHOUSE ===");
            Console.WriteLine();

            try
            {
                Console.Write("Enter Warehouse ID: ");
                if (!int.TryParse(Console.ReadLine(), out int id))
                {
                    Console.WriteLine("✗ Invalid ID. Please enter a numeric value.");
                    Console.WriteLine("Press any key to continue...");
                    Console.ReadKey();
                    return;
                }

                var warehouse = _warehouseService.GetById(id);
                if (warehouse == null)
                {
                    Console.WriteLine($"✗ Warehouse with ID {id} not found.");
                    Console.WriteLine("Press any key to continue...");
                    Console.ReadKey();
                    return;
                }

                Console.Write($"Are you sure you want to delete warehouse '{warehouse.Name}'? (Y/N): ");
                var confirmation = Console.ReadLine()?.ToUpper();

                if (confirmation == "Y")
                {
                    _warehouseService.Delete(id);
                    Console.WriteLine("✓ Warehouse deleted successfully!");
                }
                else
                {
                    Console.WriteLine("Delete operation cancelled.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"✗ Error: {ex.Message}");
            }

            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
        }

        private void GetAllWarehouses()
        {
            Console.Clear();
            Console.WriteLine("=== ALL WAREHOUSES ===");
            Console.WriteLine();

            try
            {
                var warehouses = _warehouseService.GetAll();
                if (!warehouses.Any())
                {
                    Console.WriteLine("No warehouses found.");
                }
                else
                {
                    Console.WriteLine(warehouses.ToJson());
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"✗ Error: {ex.Message}");
            }

            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
        }

        private void GetWarehouseById()
        {
            Console.Clear();
            Console.WriteLine("=== GET WAREHOUSE BY ID ===");
            Console.WriteLine();

            try
            {
                Console.Write("Enter Warehouse ID: ");
                if (!int.TryParse(Console.ReadLine(), out int id))
                {
                    Console.WriteLine("✗ Invalid ID. Please enter a numeric value.");
                    Console.WriteLine("Press any key to continue...");
                    Console.ReadKey();
                    return;
                }

                var warehouse = _warehouseService.GetById(id);
                if (warehouse == null)
                {
                    Console.WriteLine($"✗ Warehouse with ID {id} not found.");
                }
                else
                {
                    Console.WriteLine(warehouse.ToJson());
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"✗ Error: {ex.Message}");
            }

            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
        }

        #endregion

        #region Inventory Operations

        private void InventoryOperationsMenu()
        {
            bool back = false;
            while (!back)
            {
                Console.Clear();
                Console.WriteLine("╔════════════════════════════════════════╗");
                Console.WriteLine("║       INVENTORY OPERATIONS             ║");
                Console.WriteLine("╚════════════════════════════════════════╝");
                Console.WriteLine();
                Console.WriteLine("1. Register Entry (Inbound)");
                Console.WriteLine("2. Register Exit (Outbound)");
                Console.WriteLine("3. Register Positive Adjustment");
                Console.WriteLine("4. Register Negative Adjustment");
                Console.WriteLine("5. Get Stock by Product");
                Console.WriteLine("6. Get Stock by Product and Warehouse");
                Console.WriteLine("7. Get Movements by Product");
                Console.WriteLine("8. Get Movements by Warehouse");
                Console.WriteLine("9. Get Movements by Product and Warehouse");
                Console.WriteLine("10. Back to Main Menu");
                Console.WriteLine();
                Console.Write("Select an option: ");

                var choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        RegisterEntry();
                        break;
                    case "2":
                        RegisterExit();
                        break;
                    case "3":
                        RegisterPositiveAdjustment();
                        break;
                    case "4":
                        RegisterNegativeAdjustment();
                        break;
                    case "5":
                        GetStockByProduct();
                        break;
                    case "6":
                        GetStockByProductAndWarehouse();
                        break;
                    case "7":
                        GetMovementsByProduct();
                        break;
                    case "8":
                        GetMovementsByWarehouse();
                        break;
                    case "9":
                        GetMovementsByProductAndWarehouse();
                        break;
                    case "10":
                        back = true;
                        break;
                    default:
                        Console.WriteLine("✗ Invalid option. Please try again.");
                        Console.WriteLine("Press any key to continue...");
                        Console.ReadKey();
                        break;
                }
            }
        }

        private void RegisterEntry()
        {
            Console.Clear();
            Console.WriteLine("=== REGISTER ENTRY (INBOUND) ===");
            Console.WriteLine();

            try
            {
                Console.Write("Enter Product ID: ");
                if (!int.TryParse(Console.ReadLine(), out int productId))
                {
                    Console.WriteLine("✗ Invalid Product ID. Please enter a numeric value.");
                    Console.WriteLine("Press any key to continue...");
                    Console.ReadKey();
                    return;
                }

                Console.Write("Enter Warehouse ID: ");
                if (!int.TryParse(Console.ReadLine(), out int warehouseId))
                {
                    Console.WriteLine("✗ Invalid Warehouse ID. Please enter a numeric value.");
                    Console.WriteLine("Press any key to continue...");
                    Console.ReadKey();
                    return;
                }

                Console.Write("Enter Quantity: ");
                if (!int.TryParse(Console.ReadLine(), out int quantity))
                {
                    Console.WriteLine("✗ Invalid Quantity. Please enter a numeric value.");
                    Console.WriteLine("Press any key to continue...");
                    Console.ReadKey();
                    return;
                }

                _inventoryService.RegisterEntry(productId, warehouseId, quantity);
                Console.WriteLine("✓ Entry registered successfully!");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"✗ Error: {ex.Message}");
            }

            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
        }

        private void RegisterExit()
        {
            Console.Clear();
            Console.WriteLine("=== REGISTER EXIT (OUTBOUND) ===");
            Console.WriteLine();

            try
            {
                Console.Write("Enter Product ID: ");
                if (!int.TryParse(Console.ReadLine(), out int productId))
                {
                    Console.WriteLine("✗ Invalid Product ID. Please enter a numeric value.");
                    Console.WriteLine("Press any key to continue...");
                    Console.ReadKey();
                    return;
                }

                Console.Write("Enter Warehouse ID: ");
                if (!int.TryParse(Console.ReadLine(), out int warehouseId))
                {
                    Console.WriteLine("✗ Invalid Warehouse ID. Please enter a numeric value.");
                    Console.WriteLine("Press any key to continue...");
                    Console.ReadKey();
                    return;
                }

                Console.Write("Enter Quantity: ");
                if (!int.TryParse(Console.ReadLine(), out int quantity))
                {
                    Console.WriteLine("✗ Invalid Quantity. Please enter a numeric value.");
                    Console.WriteLine("Press any key to continue...");
                    Console.ReadKey();
                    return;
                }

                _inventoryService.RegisterExit(productId, warehouseId, quantity);
                Console.WriteLine("✓ Exit registered successfully!");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"✗ Error: {ex.Message}");
            }

            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
        }

        private void RegisterPositiveAdjustment()
        {
            Console.Clear();
            Console.WriteLine("=== REGISTER POSITIVE ADJUSTMENT ===");
            Console.WriteLine();

            try
            {
                Console.Write("Enter Product ID: ");
                if (!int.TryParse(Console.ReadLine(), out int productId))
                {
                    Console.WriteLine("✗ Invalid Product ID. Please enter a numeric value.");
                    Console.WriteLine("Press any key to continue...");
                    Console.ReadKey();
                    return;
                }

                Console.Write("Enter Warehouse ID: ");
                if (!int.TryParse(Console.ReadLine(), out int warehouseId))
                {
                    Console.WriteLine("✗ Invalid Warehouse ID. Please enter a numeric value.");
                    Console.WriteLine("Press any key to continue...");
                    Console.ReadKey();
                    return;
                }

                Console.Write("Enter Quantity: ");
                if (!int.TryParse(Console.ReadLine(), out int quantity))
                {
                    Console.WriteLine("✗ Invalid Quantity. Please enter a numeric value.");
                    Console.WriteLine("Press any key to continue...");
                    Console.ReadKey();
                    return;
                }

                _inventoryService.RegisterPositiveAdjustment(productId, warehouseId, quantity);
                Console.WriteLine("✓ Positive adjustment registered successfully!");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"✗ Error: {ex.Message}");
            }

            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
        }

        private void RegisterNegativeAdjustment()
        {
            Console.Clear();
            Console.WriteLine("=== REGISTER NEGATIVE ADJUSTMENT ===");
            Console.WriteLine();

            try
            {
                Console.Write("Enter Product ID: ");
                if (!int.TryParse(Console.ReadLine(), out int productId))
                {
                    Console.WriteLine("✗ Invalid Product ID. Please enter a numeric value.");
                    Console.WriteLine("Press any key to continue...");
                    Console.ReadKey();
                    return;
                }

                Console.Write("Enter Warehouse ID: ");
                if (!int.TryParse(Console.ReadLine(), out int warehouseId))
                {
                    Console.WriteLine("✗ Invalid Warehouse ID. Please enter a numeric value.");
                    Console.WriteLine("Press any key to continue...");
                    Console.ReadKey();
                    return;
                }

                Console.Write("Enter Quantity: ");
                if (!int.TryParse(Console.ReadLine(), out int quantity))
                {
                    Console.WriteLine("✗ Invalid Quantity. Please enter a numeric value.");
                    Console.WriteLine("Press any key to continue...");
                    Console.ReadKey();
                    return;
                }

                _inventoryService.RegisterNegativeAdjustment(productId, warehouseId, quantity);
                Console.WriteLine("✓ Negative adjustment registered successfully!");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"✗ Error: {ex.Message}");
            }

            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
        }

        private void GetStockByProduct()
        {
            Console.Clear();
            Console.WriteLine("=== GET STOCK BY PRODUCT ===");
            Console.WriteLine();

            try
            {
                Console.Write("Enter Product ID: ");
                if (!int.TryParse(Console.ReadLine(), out int productId))
                {
                    Console.WriteLine("✗ Invalid Product ID. Please enter a numeric value.");
                    Console.WriteLine("Press any key to continue...");
                    Console.ReadKey();
                    return;
                }

                var stock = _inventoryService.GetStock(productId);
                Console.WriteLine($"Total stock for Product ID {productId}: {stock}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"✗ Error: {ex.Message}");
            }

            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
        }

        private void GetStockByProductAndWarehouse()
        {
            Console.Clear();
            Console.WriteLine("=== GET STOCK BY PRODUCT AND WAREHOUSE ===");
            Console.WriteLine();

            try
            {
                Console.Write("Enter Product ID: ");
                if (!int.TryParse(Console.ReadLine(), out int productId))
                {
                    Console.WriteLine("✗ Invalid Product ID. Please enter a numeric value.");
                    Console.WriteLine("Press any key to continue...");
                    Console.ReadKey();
                    return;
                }

                Console.Write("Enter Warehouse ID: ");
                if (!int.TryParse(Console.ReadLine(), out int warehouseId))
                {
                    Console.WriteLine("✗ Invalid Warehouse ID. Please enter a numeric value.");
                    Console.WriteLine("Press any key to continue...");
                    Console.ReadKey();
                    return;
                }

                var stock = _inventoryService.GetStockByWarehouse(productId, warehouseId);
                Console.WriteLine($"Stock for Product ID {productId} in Warehouse ID {warehouseId}: {stock}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"✗ Error: {ex.Message}");
            }

            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
        }

        private void GetMovementsByProduct()
        {
            Console.Clear();
            Console.WriteLine("=== GET MOVEMENTS BY PRODUCT ===");
            Console.WriteLine();

            try
            {
                Console.Write("Enter Product ID: ");
                if (!int.TryParse(Console.ReadLine(), out int productId))
                {
                    Console.WriteLine("✗ Invalid Product ID. Please enter a numeric value.");
                    Console.WriteLine("Press any key to continue...");
                    Console.ReadKey();
                    return;
                }

                var movements = _inventoryService.GetMovementsByProduct(productId);
                if (!movements.Any())
                {
                    Console.WriteLine($"No movements found for Product ID {productId}.");
                }
                else
                {
                    Console.WriteLine(movements.ToJson());
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"✗ Error: {ex.Message}");
            }

            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
        }

        private void GetMovementsByWarehouse()
        {
            Console.Clear();
            Console.WriteLine("=== GET MOVEMENTS BY WAREHOUSE ===");
            Console.WriteLine();

            try
            {
                Console.Write("Enter Warehouse ID: ");
                if (!int.TryParse(Console.ReadLine(), out int warehouseId))
                {
                    Console.WriteLine("✗ Invalid Warehouse ID. Please enter a numeric value.");
                    Console.WriteLine("Press any key to continue...");
                    Console.ReadKey();
                    return;
                }

                var movements = _inventoryService.GetMovementsByWarehouse(warehouseId);
                if (!movements.Any())
                {
                    Console.WriteLine($"No movements found for Warehouse ID {warehouseId}.");
                }
                else
                {
                    Console.WriteLine(movements.ToJson());
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"✗ Error: {ex.Message}");
            }

            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
        }

        private void GetMovementsByProductAndWarehouse()
        {
            Console.Clear();
            Console.WriteLine("=== GET MOVEMENTS BY PRODUCT AND WAREHOUSE ===");
            Console.WriteLine();

            try
            {
                Console.Write("Enter Product ID: ");
                if (!int.TryParse(Console.ReadLine(), out int productId))
                {
                    Console.WriteLine("✗ Invalid Product ID. Please enter a numeric value.");
                    Console.WriteLine("Press any key to continue...");
                    Console.ReadKey();
                    return;
                }

                Console.Write("Enter Warehouse ID: ");
                if (!int.TryParse(Console.ReadLine(), out int warehouseId))
                {
                    Console.WriteLine("✗ Invalid Warehouse ID. Please enter a numeric value.");
                    Console.WriteLine("Press any key to continue...");
                    Console.ReadKey();
                    return;
                }

                var movements = _inventoryService.GetMovementsByProductAndWarehouse(productId, warehouseId);
                if (!movements.Any())
                {
                    Console.WriteLine($"No movements found for Product ID {productId} in Warehouse ID {warehouseId}.");
                }
                else
                {
                    Console.WriteLine(movements.ToJson());
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"✗ Error: {ex.Message}");
            }

            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
        }

        #endregion
    }
}
