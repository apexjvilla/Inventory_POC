using Inventory.Domain.Enums;
using System.Diagnostics;

namespace Inventory.Domain.Entities
{
    public class Movement : EntityBase
    {
        public Movement(int productId, int warehouseId, int quantity, DateTime date, MovementType movementType)
        {
            if (productId <= 0)
                throw new ArgumentException($"Invalid {nameof(ProductId)}");

            if (warehouseId <= 0)
                throw new ArgumentException($"Invalid {nameof(WarehouseId)}");

            if (quantity <= 0)
                throw new ArgumentException($"{nameof(Quantity)} can not be 0");

            ProductId = productId;
            WarehouseId = warehouseId;
            Quantity = quantity;
            Date = date;
            MovementType = movementType;
        }

        public int ProductId { get; private set; }
        public int WarehouseId { get; private set; }
        public int Quantity { get; private set; }
        public DateTime Date { get; private set; }
        public MovementType MovementType { get; set; }
    }
}
