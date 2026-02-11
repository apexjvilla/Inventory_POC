using Inventory.Domain.Enums;

namespace Inventory.Domain.Entities
{
    public class Movement : EntityBase
    {
        public int ProductId { get; set; }
        public int WarehouseId { get; set; }
        public int Quantity { get; set; }
        public DateTime Date { get; set; }
        public MovementType MovementType { get; set; }
    }
}
