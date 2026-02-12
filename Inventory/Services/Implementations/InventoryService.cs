using Inventory.Domain.Entities;
using Inventory.Domain.Enums;
using Inventory.Helpers;
using Inventory.Repository.Abstractions;
using Inventory.Services.Abstractions;
using System.ComponentModel.DataAnnotations;

namespace Inventory.Services.Implementations
{
    public class InventoryService(IProductRepository productRepository,
        IWarehouseRepository warehouseRepository,
        IMovementWriteRepository movementWriteRepository,
        IMovementReadRepository movementReadRepository,
        IInventoryValidationsService inventoryValidations) : IInventoryService
    {
        private readonly IProductRepository _productRepository = productRepository;
        private readonly IWarehouseRepository _warehouseRepository = warehouseRepository;
        private readonly IMovementWriteRepository _movementWriteRepository = movementWriteRepository;
        private readonly IMovementReadRepository _movementReadRepository = movementReadRepository;
        private readonly IInventoryValidationsService _inventoryValidations = inventoryValidations;

        public void RegisterEntry(int productId, int warehouseId, int quantity)
        {
            try
            {
                _inventoryValidations.ValidateProductExists(productId);

                _inventoryValidations.ValidateWarehouseExists(warehouseId);

                var movement = new Movement
                {
                    ProductId = productId,
                    WarehouseId = warehouseId,
                    Date = DateTime.Now,
                    MovementType = MovementType.Inbound
                };

                _movementWriteRepository.Add(movement);
            }
            catch (Exception ex)
            {
                Logger.LogError(ex);
                throw;
            }
        }

        public void RegisterExit(int productId, int warehouseId, int quantity)
        {
            try
            {
                _inventoryValidations.ValidateProductExists(productId);

                _inventoryValidations.ValidateWarehouseExists(warehouseId);

                if (!_inventoryValidations.ValidateProductAndWarehouseStock(productId, warehouseId, quantity))
                    throw new ValidationException("Insufficient stock for the product in the warehouse.");

                var movement = new Movement
                {
                    ProductId = productId,
                    WarehouseId = warehouseId,
                    Date = DateTime.Now,
                    MovementType = MovementType.Outbound
                };

                _movementWriteRepository.Add(movement);
            }
            catch (Exception ex)
            {
                Logger.LogError(ex);
                throw;
            }
        }

        public void RegisterPositiveAdjustment(int productId, int warehouseId, int quantity)
        {
            try
            {
                _inventoryValidations.ValidateProductExists(productId);

                _inventoryValidations.ValidateWarehouseExists(warehouseId);

                var movement = new Movement
                {
                    ProductId = productId,
                    WarehouseId = warehouseId,
                    Date = DateTime.Now,
                    MovementType = MovementType.PositiveAdjustment
                };

                _movementWriteRepository.Add(movement);
            }
            catch (Exception ex)
            {
                Logger.LogError(ex);
                throw;
            }
        }

        public void RegisterNegativeAdjustment(int productId, int warehouseId, int quantity)
        {
            try
            {
                _inventoryValidations.ValidateProductExists(productId);

                _inventoryValidations.ValidateWarehouseExists(warehouseId);

                var movement = new Movement
                {
                    ProductId = productId,
                    WarehouseId = warehouseId,
                    Date = DateTime.Now,
                    MovementType = MovementType.NegativeAdjustment
                };

                _movementWriteRepository.Add(movement);
            }
            catch (Exception ex)
            {
                Logger.LogError(ex);
                throw;
            }
        }

        public int GetStock(int productId)
        {
            try
            {
                _inventoryValidations.ValidateProductExists(productId);

                return _movementReadRepository.GetAll()
                    .Where(x => x.ProductId == productId)
                    .Sum(x => x.Quantity);
            }
            catch (Exception ex)
            {
                Logger.LogError(ex);
                throw;
            }
        }

        public int GetStockByWarehouse(int productId, int warehouseId)
        {
            try
            {
                _inventoryValidations.ValidateProductExists(productId);

                _inventoryValidations.ValidateWarehouseExists(warehouseId);

                return _movementReadRepository.GetAll()
                    .Where(x => x.ProductId == productId
                        && x.WarehouseId == warehouseId)
                    .Sum(x => x.Quantity);
            }
            catch (Exception ex)
            {
                Logger.LogError(ex);
                throw;
            }
        }

        public IReadOnlyList<Movement> GetMovementsByProduct(int warehouseId)
        {
            try
            {
                _inventoryValidations.ValidateProductExists(warehouseId);

                return _movementReadRepository.GetAll()
                    .Where(x => x.ProductId == warehouseId)
                    .ToList()
                    .AsReadOnly();
            }
            catch (Exception ex)
            {
                Logger.LogError(ex);
                throw;
            }
        }

        public IReadOnlyList<Movement> GetMovementsByWarehouse(int warehouseId)
        {
            try
            {
                _inventoryValidations.ValidateWarehouseExists(warehouseId);

                return _movementReadRepository.GetAll()
                    .Where(x => x.WarehouseId == warehouseId)
                    .ToList()
                    .AsReadOnly();
            }
            catch (Exception ex)
            {
                Logger.LogError(ex);
                throw;
            }
        }

        public IReadOnlyList<Movement> GetMovementsByProductAndWarehouse(int productId, int warehouseId)
        {
            try
            {
                _inventoryValidations.ValidateProductExists(productId);

                _inventoryValidations.ValidateWarehouseExists(warehouseId);

                return _movementReadRepository.GetAll()
                    .Where(x => x.ProductId == productId
                        && x.WarehouseId == warehouseId)
                    .ToList()
                    .AsReadOnly();
            }
            catch (Exception ex)
            {
                Logger.LogError(ex);
                throw;
            }
        }
    }
}
