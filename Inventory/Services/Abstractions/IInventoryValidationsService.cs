namespace Inventory.Services.Abstractions
{
    public interface IInventoryValidationsService
    {
        void ValidateProductExists(int productId);
        void ValidateWarehouseExists(int warehouseId);
        bool ValidateProductAndWarehouseStock(int productId, int warehouseId, int quantity);
    }
}
