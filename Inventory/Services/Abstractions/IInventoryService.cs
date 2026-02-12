using Inventory.Domain.Entities;

namespace Inventory.Services.Abstractions
{
    public interface IInventoryService
    {
        void RegisterEntry(int productId, int warehouseId, int quantity);
        void RegisterExit(int productId, int warehouseId, int quantity);
        void RegisterPositiveAdjustment(int productId, int warehouseId, int quantity);
        void RegisterNegativeAdjustment(int productId, int warehouseId, int quantity);
        int GetStock(int productId);
        int GetStockByWarehouse(int productId, int warehouseId);
        IReadOnlyList<Movement> GetMovementsByProduct(int warehouseId);
        IReadOnlyList<Movement> GetMovementsByWarehouse(int warehouseId);
        IReadOnlyList<Movement> GetMovementsByProductAndWarehouse(int productId, int warehouseId);
    }
}