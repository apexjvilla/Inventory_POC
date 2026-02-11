using Inventory.Domain.Entities;

namespace Inventory.Services.Abstractions
{
    public interface IInventoryService1
    {
        void RegisterEntry(int productId, int warehouseId, int quantity);
        void RegisterExit(int productId, int warehouseId, int quantity);
        void RegisterPositiveAdjustment(int productId, int warehouseId, int quantity);
        void RegisterNegativeAdjustment(int productId, int warehouseId, int quantity);
        int GetStock(int productId);
        int GetStockByWarehouse(int productId, int warehouseId);
        List<Movement> GetMovementsByProduct(int warehouseId);
        List<Movement> GetMovementsByWarehouse(int warehouseId);
        List<Movement> GetMovementsByProductAndWarehouse(int productId, int warehouseId);
    }
}