using Inventory.Domain.Entities;
using Inventory.Repository.Abstractions;
using Inventory.Services.Abstractions;
using System.ComponentModel.DataAnnotations;

namespace Inventory.Services.Implementations
{
    public class InventoryValidationsService(IProductRepository productRepository,
        IWarehouseRepository warehouseRepository,
        IMovementReadRepository movementRepository) : IInventoryValidationsService
    {
        private readonly IProductRepository _productRepository = productRepository;
        private readonly IWarehouseRepository _warehouseRepository = warehouseRepository;
        private readonly IMovementReadRepository _movementRepository = movementRepository;

        public void ValidateProductExists(int productId)
        {
            bool exists = _productRepository.GetById(productId) != null;

            if (!exists)
                throw new ValidationException("Product does not exist.");
        }

        public void ValidateWarehouseExists(int warehouseId)
        {
            bool exists = _warehouseRepository.GetById(warehouseId) != null;

            if (!exists)
                throw new ValidationException("Warehouse does not exist.");
        }

        public bool ValidateProductAndWarehouseStock(int productId, int warehouseId, int quantity)
            {
            var movements = _movementRepository.GetAll()
                .Where(m => m.ProductId == productId && m.WarehouseId == warehouseId);

            int totalInbound = movements
                .Where(m => m.MovementType == Domain.Enums.MovementType.Inbound
                    || m.MovementType == Domain.Enums.MovementType.PositiveAdjustment)
                .Sum(m => m.Quantity);

            int totalOutbound = movements
                .Where(m => m.MovementType == Domain.Enums.MovementType.Outbound
                    || m.MovementType == Domain.Enums.MovementType.NegativeAdjustment)
                .Sum(m => m.Quantity);

            int currentStock = totalInbound - totalOutbound;

            return currentStock >= quantity;
        }
}
}
