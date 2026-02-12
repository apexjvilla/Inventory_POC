using Inventory.Domain.Entities;

namespace Inventory.Repository.Abstractions
{
    public interface IMovementWriteRepository
    {
        void Add(Movement movement);
    }
}
