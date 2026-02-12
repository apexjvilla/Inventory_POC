using Inventory.Domain.Entities;

namespace Inventory.Repository.Abstractions
{
    public interface IMovementReadRepository
    {
        IReadOnlyList<Movement> GetAll();
        Movement? GetById(int movementId);
    }
}
