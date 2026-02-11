namespace Inventory.Services.Abstractions
{
    public interface IInventoryValidations
    {
        void ValidateProductExists(int productId);
        void ValidateWarehouseExists(int warehouseId);
        bool ValidateProductAndWarehouseStock(int productId, int warehouseId, int quantity);
    }
}
