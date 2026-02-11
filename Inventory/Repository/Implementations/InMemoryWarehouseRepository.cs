using Inventory.Domain.Entities;
using Inventory.Repository.Abstractions;

namespace Inventory.Repository.Implementations
{
    public class InMemoryWarehouseRepository : InMemoryRepositoryBase<Warehouse>, IWarehouseRepository
    {
    }
}
