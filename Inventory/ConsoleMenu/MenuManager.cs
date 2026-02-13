using Inventory.Domain.Entities;
using Inventory.Extensions;
using Inventory.Services.Abstractions;

namespace Inventory.ConsoleMenu
{
    public class MenuManager(
        IProductService productService,
        IWarehouseService warehouseService,
        IInventoryService inventoryService)
    {
        public void Run()
        {
            while (true)
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
                        Console.WriteLine("\nExiting application. Goodbye!");
                        return;
                    default:
                        Console.WriteLine("\n✗ Invalid option. Please try again.");
                        Console.WriteLine("\nPress any key to continue...");
                        Console.ReadKey();
                        break;
                }
            }
        }

        private void ShowMainMenu()
        {
            Console.WriteLine("╔═══════════════════════════════════════════════════════╗");
            Console.WriteLine("║         INVENTORY MANAGEMENT SYSTEM                   ║");
            Console.WriteLine("╚═══════════════════════════════════════════════════════╝");
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
            while (true)
            {
                Console.Clear();
                Console.WriteLine("╔═══════════════════════════════════════════════════════╗");
                Console.WriteLine("║         PRODUCT MANAGEMENT                            ║");
                Console.WriteLine("╚═══════════════════════════════════════════════════════╝");
                Console.WriteLine();
                Console.WriteLine("1. Create Product");
                Console.WriteLine("2. Update Product");
                Console.WriteLine("3. Delete Product");
                Console.WriteLine("4. Get All Products");
                Console.WriteLine("5. Get Product by ID");
                Console.WriteLine("6. Return to Main Menu");
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
                        return;
                    default:
                        Console.WriteLine("\n✗ Invalid option. Please try again.");
                        Console.WriteLine("\nPress any key to continue...");
                        Console.ReadKey();
                        break;
                }
            }
        }

        private void CreateProduct()
        {
            Console.Clear();
            Console.WriteLine("=== Create Product ===");
            Console.WriteLine();

            try
            {
                Console.Write("Enter product name: ");
                var name = Console.ReadLine();

                Console.Write("Enter product description: ");
                var description = Console.ReadLine();

                Console.Write("Enter product price: ");
                var strPrice = Console.ReadLine();

                int price = Convert.ToInt32(strPrice);

                var product = new Product(name, description, price);

                productService.Add(product);
                Console.WriteLine("\n✓ Product created successfully!");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"\n✗ Error: {ex.Message}");
            }

            Console.WriteLine("\nPress any key to continue...");
            Console.ReadKey();
        }

        private void UpdateProduct()
        {
            Console.Clear();
            Console.WriteLine("=== Update Product ===");
            Console.WriteLine();

            try
            {
                Console.Write("Enter product ID to update: ");
                if (!int.TryParse(Console.ReadLine(), out int id))
                {
                    Console.WriteLine("\n✗ Invalid ID format. Please enter a valid number.");
                    Console.WriteLine("\nPress any key to continue...");
                    Console.ReadKey();
                    return;
                }

                var product = productService.GetById(id);
                if (product == null)
                {
                    Console.WriteLine($"\n✗ Product with ID {id} not found.");
                    Console.WriteLine("\nPress any key to continue...");
                    Console.ReadKey();
                    return;
                }

                Console.WriteLine($"\nCurrent Name: {product.Name}");
                Console.Write("Enter new name (press Enter to keep current): ");
                var name = Console.ReadLine();

                Console.WriteLine($"\nCurrent Description: {product.Description}");
                Console.Write("Enter new description (press Enter to keep current): ");
                var description = Console.ReadLine();

                Console.WriteLine($"\nCurrent price: {product.Price}");
                Console.Write("Enter new price(press Enter to keep current): ");
                var strPrice = Console.ReadLine();
                int price = Convert.ToInt32(strPrice);

                product.Update(name, description, price);

                productService.Update(product);
                Console.WriteLine("\n✓ Product updated successfully!");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"\n✗ Error: {ex.Message}");
            }

            Console.WriteLine("\nPress any key to continue...");
            Console.ReadKey();
        }

        private void DeleteProduct()
        {
            Console.Clear();
            Console.WriteLine("=== Delete Product ===");
            Console.WriteLine();

            try
            {
                Console.Write("Enter product ID to delete: ");
                if (!int.TryParse(Console.ReadLine(), out int id))
                {
                    Console.WriteLine("\n✗ Invalid ID format. Please enter a valid number.");
                    Console.WriteLine("\nPress any key to continue...");
                    Console.ReadKey();
                    return;
                }

                var product = productService.GetById(id);
                if (product == null)
                {
                    Console.WriteLine($"\n✗ Product with ID {id} not found.");
                    Console.WriteLine("\nPress any key to continue...");
                    Console.ReadKey();
                    return;
                }

                Console.WriteLine($"\nProduct: {product.Name}");
                Console.Write("Are you sure you want to delete this product? (y/n): ");
                var confirmation = Console.ReadLine()?.ToLower();

                if (confirmation == "y" || confirmation == "yes")
                {
                    productService.Delete(id);
                    Console.WriteLine("\n✓ Product deleted successfully!");
                }
                else
                {
                    Console.WriteLine("\n✗ Deletion cancelled.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"\n✗ Error: {ex.Message}");
            }

            Console.WriteLine("\nPress any key to continue...");
            Console.ReadKey();
        }

        private void GetAllProducts()
        {
            Console.Clear();
            Console.WriteLine("=== All Products ===");
            Console.WriteLine();

            try
            {
                var products = productService.GetAll();
                Console.WriteLine(products.ToJson());
            }
            catch (Exception ex)
            {
                Console.WriteLine($"\n✗ Error: {ex.Message}");
            }

            Console.WriteLine("\nPress any key to continue...");
            Console.ReadKey();
        }

        private void GetProductById()
        {
            Console.Clear();
            Console.WriteLine("=== Get Product by ID ===");
            Console.WriteLine();

            try
            {
                Console.Write("Enter product ID: ");
                if (!int.TryParse(Console.ReadLine(), out int id))
                {
                    Console.WriteLine("\n✗ Invalid ID format. Please enter a valid number.");
                    Console.WriteLine("\nPress any key to continue...");
                    Console.ReadKey();
                    return;
                }

                var product = productService.GetById(id);
                if (product == null)
                {
                    Console.WriteLine($"\n✗ Product with ID {id} not found.");
                }
                else
                {
                    Console.WriteLine(product.ToJson());
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"\n✗ Error: {ex.Message}");
            }

            Console.WriteLine("\nPress any key to continue...");
            Console.ReadKey();
        }

        #endregion

        #region Warehouse Management

        private void WarehouseManagementMenu()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("╔═══════════════════════════════════════════════════════╗");
                Console.WriteLine("║         WAREHOUSE MANAGEMENT                          ║");
                Console.WriteLine("╚═══════════════════════════════════════════════════════╝");
                Console.WriteLine();
                Console.WriteLine("1. Create Warehouse");
                Console.WriteLine("2. Update Warehouse");
                Console.WriteLine("3. Delete Warehouse");
                Console.WriteLine("4. Get All Warehouses");
                Console.WriteLine("5. Get Warehouse by ID");
                Console.WriteLine("6. Return to Main Menu");
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
                        return;
                    default:
                        Console.WriteLine("\n✗ Invalid option. Please try again.");
                        Console.WriteLine("\nPress any key to continue...");
                        Console.ReadKey();
                        break;
                }
            }
        }

        private void CreateWarehouse()
        {
            Console.Clear();
            Console.WriteLine("=== Create Warehouse ===");
            Console.WriteLine();

            try
            {
                Console.Write("Enter warehouse name: ");
                var name = Console.ReadLine();

                Console.Write("Enter warehouse location: ");
                var location = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(name))
                {
                    Console.WriteLine("\n✗ Warehouse name cannot be empty.");
                    Console.WriteLine("\nPress any key to continue...");
                    Console.ReadKey();
                    return;
                }

                var warehouse = new Warehouse(name, location);

                warehouseService.Add(warehouse);
                Console.WriteLine("\n✓ Warehouse created successfully!");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"\n✗ Error: {ex.Message}");
            }

            Console.WriteLine("\nPress any key to continue...");
            Console.ReadKey();
        }

        private void UpdateWarehouse()
        {
            Console.Clear();
            Console.WriteLine("=== Update Warehouse ===");
            Console.WriteLine();

            try
            {
                Console.Write("Enter warehouse ID to update: ");
                if (!int.TryParse(Console.ReadLine(), out int id))
                {
                    Console.WriteLine("\n✗ Invalid ID format. Please enter a valid number.");
                    Console.WriteLine("\nPress any key to continue...");
                    Console.ReadKey();
                    return;
                }

                var warehouse = warehouseService.GetById(id);
                if (warehouse == null)
                {
                    Console.WriteLine($"\n✗ Warehouse with ID {id} not found.");
                    Console.WriteLine("\nPress any key to continue...");
                    Console.ReadKey();
                    return;
                }

                Console.WriteLine($"\nCurrent Name: {warehouse.Name}");
                Console.Write("Enter new name (press Enter to keep current): ");
                var name = Console.ReadLine();

                Console.WriteLine($"\nCurrent Location: {warehouse.Location}");
                Console.Write("Enter new location (press Enter to keep current): ");
                var location = Console.ReadLine();

                warehouse.Update(name, location);

                warehouseService.Update(warehouse);
                Console.WriteLine("\n✓ Warehouse updated successfully!");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"\n✗ Error: {ex.Message}");
            }

            Console.WriteLine("\nPress any key to continue...");
            Console.ReadKey();
        }

        private void DeleteWarehouse()
        {
            Console.Clear();
            Console.WriteLine("=== Delete Warehouse ===");
            Console.WriteLine();

            try
            {
                Console.Write("Enter warehouse ID to delete: ");
                if (!int.TryParse(Console.ReadLine(), out int id))
                {
                    Console.WriteLine("\n✗ Invalid ID format. Please enter a valid number.");
                    Console.WriteLine("\nPress any key to continue...");
                    Console.ReadKey();
                    return;
                }

                var warehouse = warehouseService.GetById(id);
                if (warehouse == null)
                {
                    Console.WriteLine($"\n✗ Warehouse with ID {id} not found.");
                    Console.WriteLine("\nPress any key to continue...");
                    Console.ReadKey();
                    return;
                }

                Console.WriteLine($"\nWarehouse: {warehouse.Name}");
                Console.Write("Are you sure you want to delete this warehouse? (y/n): ");
                var confirmation = Console.ReadLine()?.ToLower();

                if (confirmation == "y" || confirmation == "yes")
                {
                    warehouseService.Delete(id);
                    Console.WriteLine("\n✓ Warehouse deleted successfully!");
                }
                else
                {
                    Console.WriteLine("\n✗ Deletion cancelled.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"\n✗ Error: {ex.Message}");
            }

            Console.WriteLine("\nPress any key to continue...");
            Console.ReadKey();
        }

        private void GetAllWarehouses()
        {
            Console.Clear();
            Console.WriteLine("=== All Warehouses ===");
            Console.WriteLine();

            try
            {
                var warehouses = warehouseService.GetAll();
                Console.WriteLine(warehouses.ToJson());
            }
            catch (Exception ex)
            {
                Console.WriteLine($"\n✗ Error: {ex.Message}");
            }

            Console.WriteLine("\nPress any key to continue...");
            Console.ReadKey();
        }

        private void GetWarehouseById()
        {
            Console.Clear();
            Console.WriteLine("=== Get Warehouse by ID ===");
            Console.WriteLine();

            try
            {
                Console.Write("Enter warehouse ID: ");
                if (!int.TryParse(Console.ReadLine(), out int id))
                {
                    Console.WriteLine("\n✗ Invalid ID format. Please enter a valid number.");
                    Console.WriteLine("\nPress any key to continue...");
                    Console.ReadKey();
                    return;
                }

                var warehouse = warehouseService.GetById(id);
                if (warehouse == null)
                {
                    Console.WriteLine($"\n✗ Warehouse with ID {id} not found.");
                }
                else
                {
                    Console.WriteLine(warehouse.ToJson());
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"\n✗ Error: {ex.Message}");
            }

            Console.WriteLine("\nPress any key to continue...");
            Console.ReadKey();
        }

        #endregion

        #region Inventory Operations

        private void InventoryOperationsMenu()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("╔═══════════════════════════════════════════════════════╗");
                Console.WriteLine("║         INVENTORY OPERATIONS                          ║");
                Console.WriteLine("╚═══════════════════════════════════════════════════════╝");
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
                Console.WriteLine("10. Return to Main Menu");
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
                        return;
                    default:
                        Console.WriteLine("\n✗ Invalid option. Please try again.");
                        Console.WriteLine("\nPress any key to continue...");
                        Console.ReadKey();
                        break;
                }
            }
        }

        private void RegisterEntry()
        {
            Console.Clear();
            Console.WriteLine("=== Register Entry (Inbound) ===");
            Console.WriteLine();

            try
            {
                Console.Write("Enter product ID: ");
                if (!int.TryParse(Console.ReadLine(), out int productId))
                {
                    Console.WriteLine("\n✗ Invalid product ID format.");
                    Console.WriteLine("\nPress any key to continue...");
                    Console.ReadKey();
                    return;
                }

                Console.Write("Enter warehouse ID: ");
                if (!int.TryParse(Console.ReadLine(), out int warehouseId))
                {
                    Console.WriteLine("\n✗ Invalid warehouse ID format.");
                    Console.WriteLine("\nPress any key to continue...");
                    Console.ReadKey();
                    return;
                }

                Console.Write("Enter quantity: ");
                if (!int.TryParse(Console.ReadLine(), out int quantity) || quantity <= 0)
                {
                    Console.WriteLine("\n✗ Invalid quantity. Must be a positive number.");
                    Console.WriteLine("\nPress any key to continue...");
                    Console.ReadKey();
                    return;
                }

                inventoryService.RegisterEntry(productId, warehouseId, quantity);
                Console.WriteLine("\n✓ Entry registered successfully!");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"\n✗ Error: {ex.Message}");
            }

            Console.WriteLine("\nPress any key to continue...");
            Console.ReadKey();
        }

        private void RegisterExit()
        {
            Console.Clear();
            Console.WriteLine("=== Register Exit (Outbound) ===");
            Console.WriteLine();

            try
            {
                Console.Write("Enter product ID: ");
                if (!int.TryParse(Console.ReadLine(), out int productId))
                {
                    Console.WriteLine("\n✗ Invalid product ID format.");
                    Console.WriteLine("\nPress any key to continue...");
                    Console.ReadKey();
                    return;
                }

                Console.Write("Enter warehouse ID: ");
                if (!int.TryParse(Console.ReadLine(), out int warehouseId))
                {
                    Console.WriteLine("\n✗ Invalid warehouse ID format.");
                    Console.WriteLine("\nPress any key to continue...");
                    Console.ReadKey();
                    return;
                }

                Console.Write("Enter quantity: ");
                if (!int.TryParse(Console.ReadLine(), out int quantity) || quantity <= 0)
                {
                    Console.WriteLine("\n✗ Invalid quantity. Must be a positive number.");
                    Console.WriteLine("\nPress any key to continue...");
                    Console.ReadKey();
                    return;
                }

                inventoryService.RegisterExit(productId, warehouseId, quantity);
                Console.WriteLine("\n✓ Exit registered successfully!");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"\n✗ Error: {ex.Message}");
            }

            Console.WriteLine("\nPress any key to continue...");
            Console.ReadKey();
        }

        private void RegisterPositiveAdjustment()
        {
            Console.Clear();
            Console.WriteLine("=== Register Positive Adjustment ===");
            Console.WriteLine();

            try
            {
                Console.Write("Enter product ID: ");
                if (!int.TryParse(Console.ReadLine(), out int productId))
                {
                    Console.WriteLine("\n✗ Invalid product ID format.");
                    Console.WriteLine("\nPress any key to continue...");
                    Console.ReadKey();
                    return;
                }

                Console.Write("Enter warehouse ID: ");
                if (!int.TryParse(Console.ReadLine(), out int warehouseId))
                {
                    Console.WriteLine("\n✗ Invalid warehouse ID format.");
                    Console.WriteLine("\nPress any key to continue...");
                    Console.ReadKey();
                    return;
                }

                Console.Write("Enter quantity: ");
                if (!int.TryParse(Console.ReadLine(), out int quantity) || quantity <= 0)
                {
                    Console.WriteLine("\n✗ Invalid quantity. Must be a positive number.");
                    Console.WriteLine("\nPress any key to continue...");
                    Console.ReadKey();
                    return;
                }

                inventoryService.RegisterPositiveAdjustment(productId, warehouseId, quantity);
                Console.WriteLine("\n✓ Positive adjustment registered successfully!");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"\n✗ Error: {ex.Message}");
            }

            Console.WriteLine("\nPress any key to continue...");
            Console.ReadKey();
        }

        private void RegisterNegativeAdjustment()
        {
            Console.Clear();
            Console.WriteLine("=== Register Negative Adjustment ===");
            Console.WriteLine();

            try
            {
                Console.Write("Enter product ID: ");
                if (!int.TryParse(Console.ReadLine(), out int productId))
                {
                    Console.WriteLine("\n✗ Invalid product ID format.");
                    Console.WriteLine("\nPress any key to continue...");
                    Console.ReadKey();
                    return;
                }

                Console.Write("Enter warehouse ID: ");
                if (!int.TryParse(Console.ReadLine(), out int warehouseId))
                {
                    Console.WriteLine("\n✗ Invalid warehouse ID format.");
                    Console.WriteLine("\nPress any key to continue...");
                    Console.ReadKey();
                    return;
                }

                Console.Write("Enter quantity: ");
                if (!int.TryParse(Console.ReadLine(), out int quantity) || quantity <= 0)
                {
                    Console.WriteLine("\n✗ Invalid quantity. Must be a positive number.");
                    Console.WriteLine("\nPress any key to continue...");
                    Console.ReadKey();
                    return;
                }

                inventoryService.RegisterNegativeAdjustment(productId, warehouseId, quantity);
                Console.WriteLine("\n✓ Negative adjustment registered successfully!");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"\n✗ Error: {ex.Message}");
            }

            Console.WriteLine("\nPress any key to continue...");
            Console.ReadKey();
        }

        private void GetStockByProduct()
        {
            Console.Clear();
            Console.WriteLine("=== Get Stock by Product ===");
            Console.WriteLine();

            try
            {
                Console.Write("Enter product ID: ");
                if (!int.TryParse(Console.ReadLine(), out int productId))
                {
                    Console.WriteLine("\n✗ Invalid product ID format.");
                    Console.WriteLine("\nPress any key to continue...");
                    Console.ReadKey();
                    return;
                }

                var stock = inventoryService.GetStock(productId);
                Console.WriteLine($"\nTotal stock for product ID {productId}: {stock}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"\n✗ Error: {ex.Message}");
            }

            Console.WriteLine("\nPress any key to continue...");
            Console.ReadKey();
        }

        private void GetStockByProductAndWarehouse()
        {
            Console.Clear();
            Console.WriteLine("=== Get Stock by Product and Warehouse ===");
            Console.WriteLine();

            try
            {
                Console.Write("Enter product ID: ");
                if (!int.TryParse(Console.ReadLine(), out int productId))
                {
                    Console.WriteLine("\n✗ Invalid product ID format.");
                    Console.WriteLine("\nPress any key to continue...");
                    Console.ReadKey();
                    return;
                }

                Console.Write("Enter warehouse ID: ");
                if (!int.TryParse(Console.ReadLine(), out int warehouseId))
                {
                    Console.WriteLine("\n✗ Invalid warehouse ID format.");
                    Console.WriteLine("\nPress any key to continue...");
                    Console.ReadKey();
                    return;
                }

                var stock = inventoryService.GetStockByWarehouse(productId, warehouseId);
                Console.WriteLine($"\nStock for product ID {productId} in warehouse ID {warehouseId}: {stock}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"\n✗ Error: {ex.Message}");
            }

            Console.WriteLine("\nPress any key to continue...");
            Console.ReadKey();
        }

        private void GetMovementsByProduct()
        {
            Console.Clear();
            Console.WriteLine("=== Get Movements by Product ===");
            Console.WriteLine();

            try
            {
                Console.Write("Enter product ID: ");
                if (!int.TryParse(Console.ReadLine(), out int productId))
                {
                    Console.WriteLine("\n✗ Invalid product ID format.");
                    Console.WriteLine("\nPress any key to continue...");
                    Console.ReadKey();
                    return;
                }

                var movements = inventoryService.GetMovementsByProduct(productId);
                Console.WriteLine(movements.ToJson());
            }
            catch (Exception ex)
            {
                Console.WriteLine($"\n✗ Error: {ex.Message}");
            }

            Console.WriteLine("\nPress any key to continue...");
            Console.ReadKey();
        }

        private void GetMovementsByWarehouse()
        {
            Console.Clear();
            Console.WriteLine("=== Get Movements by Warehouse ===");
            Console.WriteLine();

            try
            {
                Console.Write("Enter warehouse ID: ");
                if (!int.TryParse(Console.ReadLine(), out int warehouseId))
                {
                    Console.WriteLine("\n✗ Invalid warehouse ID format.");
                    Console.WriteLine("\nPress any key to continue...");
                    Console.ReadKey();
                    return;
                }

                var movements = inventoryService.GetMovementsByWarehouse(warehouseId);
                Console.WriteLine(movements.ToJson());
            }
            catch (Exception ex)
            {
                Console.WriteLine($"\n✗ Error: {ex.Message}");
            }

            Console.WriteLine("\nPress any key to continue...");
            Console.ReadKey();
        }

        private void GetMovementsByProductAndWarehouse()
        {
            Console.Clear();
            Console.WriteLine("=== Get Movements by Product and Warehouse ===");
            Console.WriteLine();

            try
            {
                Console.Write("Enter product ID: ");
                if (!int.TryParse(Console.ReadLine(), out int productId))
                {
                    Console.WriteLine("\n✗ Invalid product ID format.");
                    Console.WriteLine("\nPress any key to continue...");
                    Console.ReadKey();
                    return;
                }

                Console.Write("Enter warehouse ID: ");
                if (!int.TryParse(Console.ReadLine(), out int warehouseId))
                {
                    Console.WriteLine("\n✗ Invalid warehouse ID format.");
                    Console.WriteLine("\nPress any key to continue...");
                    Console.ReadKey();
                    return;
                }

                var movements = inventoryService.GetMovementsByProductAndWarehouse(productId, warehouseId);
                Console.WriteLine(movements.ToJson());
            }
            catch (Exception ex)
            {
                Console.WriteLine($"\n✗ Error: {ex.Message}");
            }

            Console.WriteLine("\nPress any key to continue...");
            Console.ReadKey();
        }

        #endregion
    }
}
